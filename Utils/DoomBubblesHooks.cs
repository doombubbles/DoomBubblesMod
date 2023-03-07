using System;
using System.Linq;
using DoomBubblesMod.Common.Configs;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Misc;
using MonoMod.Cil;
using On.Terraria.GameContent.Drawing;
using Player = On.Terraria.Player;
using PlayerDrawLayers = On.Terraria.DataStructures.PlayerDrawLayers;
using Terraria.ID;

namespace DoomBubblesMod.Utils;

public class DoomBubblesHooks : ILoadable
{
    public void Load(Mod mod)
    {
        Player.UpdateLifeRegen += PlayerOnUpdateLifeRegen;
        Player.UpdateManaRegen += PlayerOnUpdateManaRegen;
        IL.Terraria.Player.Update += PlayerOnUpdate;
        On.Terraria.Main.DamageVar += MainOnDamageVar;
        On.Terraria.Main.DrawCursor += MainOnDrawCursor;

        Player.checkDPSTime += PlayerOncheckDPSTime;
        Player.getDPS += PlayerOngetDPS;
        Player.ApplyNPCOnHitEffects += PlayerOnApplyNPCOnHitEffects;

        PlayerDrawLayers.DrawPlayer_27_HeldItem += PlayerDrawLayersOnDrawPlayer_27_HeldItem;
        Player.UpdateItemDye += PlayerOnUpdateItemDye;

        TileDrawing.DrawAnimatedTile_AdjustForVisionChangers +=
            TileDrawingOnDrawAnimatedTile_AdjustForVisionChangers;
        TileDrawing.CacheSpecialDraws += TileDrawingOnCacheSpecialDraws;
    }

    private void PlayerOnApplyNPCOnHitEffects(Player.orig_ApplyNPCOnHitEffects orig, Terraria.Player self, Item sitem,
        Rectangle itemrectangle, int damage, float knockback, int npcindex, int dmgrandomized, int dmgdone)
    {
        var hasLuckyCoin = self.hasLuckyCoin;
        if (GetInstance<ServerConfig>().ProportionateLuckyCoin)
        {
            self.hasLuckyCoin = false;
        }
        orig(self, sitem, itemrectangle, damage, knockback, npcindex, dmgrandomized, dmgdone);
        self.hasLuckyCoin = hasLuckyCoin;
    }

    private static void DoSpelunkerColor(int tileX, int tileY, ref Color tileLight)
    {
        if (GetInstance<ClientConfig>().NoSpelunkerTint &&
            Main.LocalPlayer.findTreasure &&
            Main.IsTileSpelunkable(tileX, tileY))
        {
            tileLight = Color.White;
        }
    }

    private static void TileDrawingOnCacheSpecialDraws(TileDrawing.orig_CacheSpecialDraws orig,
        Terraria.GameContent.Drawing.TileDrawing self, int tileX, int tileY, TileDrawInfo drawData)
    {
        DoSpelunkerColor(tileX, tileY, ref drawData.tileLight);
        orig(self, tileX, tileY, drawData);
    }

    private static void TileDrawingOnDrawAnimatedTile_AdjustForVisionChangers(
        TileDrawing.orig_DrawAnimatedTile_AdjustForVisionChangers orig, Terraria.GameContent.Drawing.TileDrawing self,
        int i, int j, Tile typeCache, ushort typecache, short tileFrameX, short tileFrameY, ref Color tileLight,
        bool candodust)
    {
        orig(self, i, j, typeCache, typecache, tileFrameX, tileFrameY, ref tileLight, candodust);
        DoSpelunkerColor(i, j, ref tileLight);
    }

    private void PlayerOnUpdateItemDye(Player.orig_UpdateItemDye orig, Terraria.Player self, bool isNotInVanitySlot,
        bool isSetToHidden, Item armorItem, Item dyeItem)
    {
        orig(self, isNotInVanitySlot, isSetToHidden, armorItem, dyeItem);
        if (armorItem?.ModItem is SprayPaint)
        {
            self.GetDoomBubblesPlayer().weaponDye = dyeItem.dye;
        }
    }

    private void PlayerDrawLayersOnDrawPlayer_27_HeldItem(PlayerDrawLayers.orig_DrawPlayer_27_HeldItem orig,
        ref PlayerDrawSet drawinfo)
    {
        var original = drawinfo.DrawDataCache.ToArray();
        orig(ref drawinfo);
        for (var i = 0; i < drawinfo.DrawDataCache.Count; i++)
        {
            var drawData = drawinfo.DrawDataCache[i];
            if (original.Contains(drawData) ||
                !drawinfo.drawPlayer.TryGetModPlayer(out DoomBubblesPlayer doomBubblesPlayer)) continue;

            drawinfo.DrawDataCache[i] = drawData with
            {
                shader = doomBubblesPlayer.weaponDye
            };
        }
    }

    private int PlayerOngetDPS(Player.orig_getDPS orig, Terraria.Player self)
    {
        var config = GetInstance<ClientConfig>();
        if (!config.SmoothDPSReading) return orig(self);

        var timeSpan = self.dpsEnd - self.dpsStart;
        var num = timeSpan.TotalSeconds;
        if (num >= config.DpsSeconds)
        {
            self.dpsStart = DateTime.Now.AddSeconds(num / -2);
            self.dpsDamage /= 2;
            timeSpan = self.dpsEnd - self.dpsStart;
            num = timeSpan.TotalSeconds;
        }

        if (num < 1f)
            num = 1f;

        return (int) (self.dpsDamage / num);
    }

    private void PlayerOncheckDPSTime(Player.orig_checkDPSTime orig, Terraria.Player self)
    {
        var config = GetInstance<ClientConfig>();
        if (!config.SmoothDPSReading)
        {
            orig(self);
            return;
        }

        if (self.dpsStarted && (DateTime.Now - self.dpsLastHit).Seconds >= config.DpsSeconds)
            self.dpsStarted = false;
    }


    private int MainOnDamageVar(On.Terraria.Main.orig_DamageVar orig, float dmg, float luck) =>
        GetInstance<ServerConfig>().DisableDamageVariance
            ? (int) Math.Round(dmg * (1 + luck / 20))
            : orig(dmg, luck);

    public void Unload()
    {
    }


    private static void MainOnDrawCursor(On.Terraria.Main.orig_DrawCursor orig, Vector2 bonus, bool smart)
    {
        var color = Main.cursorColor;
        if (GetInstance<ClientConfig>().PermanentRainbowCursor)
        {
            Main.cursorColor = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.25f % 1f, 1f, 0.5f);
        }

        orig(bonus, smart);
        Main.cursorColor = color;
    }


    /// <summary>
    /// Remove the 400 max mana cap
    /// </summary>
    private static void PlayerOnUpdate(ILContext il)
    {
        var c = new ILCursor(il);
        if (!c.TryGotoNext(i => i.MatchLdcI4(400)))
        {
            return;
        }

        c.Index -= 2;
        for (var i = 0; i < 7; i++)
        {
            c.Remove();
        }
    }

    private static void PlayerOnUpdateManaRegen(Player.orig_UpdateManaRegen orig, Terraria.Player self)
    {
        if (self.active && self.GetDoomBubblesPlayer().sStone)
        {
            var v = self.velocity;
            self.velocity = new Vector2(0, 0);
            orig(self);
            self.velocity = v;
            return;
        }

        orig(self);
    }

    private static void PlayerOnUpdateLifeRegen(Player.orig_UpdateLifeRegen orig, Terraria.Player self)
    {
        if (self.active && self.GetDoomBubblesPlayer().sStone && GetInstance<ServerConfig>().SorcerersStoneOP)
        {
            var v = self.velocity;
            self.velocity = new Vector2(0, 0);
            orig(self);
            self.velocity = v;
            return;
        }

        orig(self);
    }
}