using System;
using DoomBubblesMod.Common.Players;
using MonoMod.Cil;
using Main = On.Terraria.Main;
using Player = On.Terraria.Player;

namespace DoomBubblesMod.Utils;

public class DoomBubblesHooks : ILoadable
{
    public void Load(Mod mod)
    {
        Player.UpdateLifeRegen += PlayerOnUpdateLifeRegen;
        Player.UpdateManaRegen += PlayerOnUpdateManaRegen;
        IL.Terraria.Player.Update += PlayerOnUpdate;
        Main.DamageVar += (_, dmg, luck) => (int) Math.Round(dmg * (1 + luck / 20));
        Main.DrawCursor += MainOnDrawCursor;
    }

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
        Terraria.Main.LocalPlayer.hasRainbowCursor = true;

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