using System;
using System.Linq;
using DoomBubblesMod.Common.Configs;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Content.Items.Misc;
using MonoMod.Cil;
using Terraria.GameContent.Drawing;

namespace DoomBubblesMod.Utils;

public class DoomBubblesHooks : ILoadable
{
    public void Load(Mod mod)
    {
        On_Player.UpdateLifeRegen += PlayerOnUpdateLifeRegen;
        On_Player.UpdateManaRegen += PlayerOnUpdateManaRegen;
        IL_Player.Update += PlayerOnUpdate;
        On_Main.DamageVar_float_float += MainOnDamageVar;
        On_Main.DrawCursor += MainOnDrawCursor;

        On_Player.checkDPSTime += PlayerOncheckDPSTime;
        On_Player.getDPS += PlayerOngetDPS;
        On_Player.ApplyNPCOnHitEffects += PlayerOnApplyNPCOnHitEffects;

        On_PlayerDrawLayers.DrawPlayer_27_HeldItem += PlayerDrawLayersOnDrawPlayer_27_HeldItem;
        On_Player.UpdateItemDye += PlayerOnUpdateItemDye;

        On_TileDrawing.DrawAnimatedTile_AdjustForVisionChangers +=
            TileDrawingOnDrawAnimatedTile_AdjustForVisionChangers;
        On_TileDrawing.CacheSpecialDraws_Part2 += TileDrawingOnCacheSpecialDraws;

        On_Player.HasUnityPotion += On_PlayerOnHasUnityPotion;
        On_Player.TakeUnityPotion += On_PlayerOnTakeUnityPotion;
    }

    private static void On_PlayerOnTakeUnityPotion(On_Player.orig_TakeUnityPotion orig, Player self)
    {
        orig(self);
        if (GetInstance<ServerConfig>().WormholeSicknessItems.Contains(ItemID.WormholePotion))
        {
            self.AddBuff(BuffType<WormholeSickness>(), self.potionDelayTime);
        }
    }

    private static bool On_PlayerOnHasUnityPotion(On_Player.orig_HasUnityPotion orig, Player self)
    {
        return orig(self) &&
               (!GetInstance<ServerConfig>().WormholeSicknessItems.Contains(ItemID.WormholePotion) ||
                !self.HasBuff<WormholeSickness>());
    }

    private static void PlayerOnApplyNPCOnHitEffects(On_Player.orig_ApplyNPCOnHitEffects orig, Player self, Item sitem,
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

    private static void TileDrawingOnCacheSpecialDraws(On_TileDrawing.orig_CacheSpecialDraws_Part2 orig,
        TileDrawing self, int tileX, int tileY, TileDrawInfo drawData, bool skipDraw)
    {
        DoSpelunkerColor(tileX, tileY, ref drawData.tileLight);
        orig(self, tileX, tileY, drawData, skipDraw);
    }

    private static void TileDrawingOnDrawAnimatedTile_AdjustForVisionChangers(
        On_TileDrawing.orig_DrawAnimatedTile_AdjustForVisionChangers orig, TileDrawing self,
        int i, int j, Tile typeCache, ushort typecache, short tileFrameX, short tileFrameY, ref Color tileLight,
        bool candodust)
    {
        orig(self, i, j, typeCache, typecache, tileFrameX, tileFrameY, ref tileLight, candodust);
        DoSpelunkerColor(i, j, ref tileLight);
    }

    private static void PlayerOnUpdateItemDye(On_Player.orig_UpdateItemDye orig, Player self, bool isNotInVanitySlot,
        bool isSetToHidden, Item armorItem, Item dyeItem)
    {
        orig(self, isNotInVanitySlot, isSetToHidden, armorItem, dyeItem);
        if (armorItem?.ModItem is SprayPaint)
        {
            self.GetDoomBubblesPlayer().weaponDye = dyeItem.dye;
        }
    }

    private static void PlayerDrawLayersOnDrawPlayer_27_HeldItem(On_PlayerDrawLayers.orig_DrawPlayer_27_HeldItem orig,
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

    private static int PlayerOngetDPS(On_Player.orig_getDPS orig, Player self)
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

    private static void PlayerOncheckDPSTime(On_Player.orig_checkDPSTime orig, Player self)
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


    private static int MainOnDamageVar(On_Main.orig_DamageVar_float_float orig, float dmg, float luck) =>
        GetInstance<ServerConfig>().DisableDamageVariance
            ? (int) Math.Round(dmg * (1 + luck / 20))
            : orig(dmg, luck);

    public void Unload()
    {
    }

    private static void MainOnDrawCursor(On_Main.orig_DrawCursor orig, Vector2 bonus, bool smart)
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

    private static void PlayerOnUpdateManaRegen(On_Player.orig_UpdateManaRegen orig, Player self)
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

    private static void PlayerOnUpdateLifeRegen(On_Player.orig_UpdateLifeRegen orig, Player self)
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