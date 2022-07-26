using System;
using System.Linq;
using DoomBubblesMod.Common.Configs;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Misc;
using MonoMod.Cil;
using Main = On.Terraria.Main;
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
        Main.DamageVar += MainOnDamageVar;
        Main.DrawCursor += MainOnDrawCursor;

        Player.checkDPSTime += PlayerOncheckDPSTime;
        Player.getDPS += PlayerOngetDPS;

        PlayerDrawLayers.DrawPlayer_27_HeldItem += PlayerDrawLayersOnDrawPlayer_27_HeldItem;
        Player.UpdateItemDye += PlayerOnUpdateItemDye;
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


    private int MainOnDamageVar(Main.orig_DamageVar orig, float dmg, float luck) =>
        GetInstance<ServerConfig>().DisableDamageVariance
            ? (int) Math.Round(dmg * (1 + luck / 20))
            : orig(dmg, luck);

    public void Unload()
    {
    }


    private static void MainOnDrawCursor(Main.orig_DrawCursor orig, Vector2 bonus, bool smart)
    {
        var gameMenu = Terraria.Main.gameMenu;
        if (gameMenu && Terraria.Main.alreadyGrabbingSunOrMoon)
        {
            return;
        }

        var hasRainbowCursor = Terraria.Main.LocalPlayer.hasRainbowCursor;

        Terraria.Main.gameMenu = false;
        Terraria.Main.LocalPlayer.hasRainbowCursor |= GetInstance<ClientConfig>().PermanentRainbowCursor;

        orig(bonus, smart);

        Terraria.Main.gameMenu = gameMenu;
        Terraria.Main.LocalPlayer.hasRainbowCursor = hasRainbowCursor;
    }


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
        if (self.active)
        {
            var sStone = self.GetModPlayer<DoomBubblesPlayer>().sStone;
            if (sStone)
            {
                var v = self.velocity;
                self.velocity = new Vector2(0, 0);
                orig(self);
                self.velocity = v;
                return;
            }
        }

        orig(self);
    }

    private static void PlayerOnUpdateLifeRegen(Player.orig_UpdateLifeRegen orig, Terraria.Player self)
    {
        if (self.active)
        {
            var sStone = self.GetModPlayer<DoomBubblesPlayer>().sStone;
            if (sStone)
            {
                var v = self.velocity;
                self.velocity = new Vector2(0, 0);
                orig(self);
                self.velocity = v;
                return;
            }
        }

        orig(self);
    }
}