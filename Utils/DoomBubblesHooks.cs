using System;
using Microsoft.Xna.Framework;
using MonoMod.Cil;

namespace DoomBubblesMod.Utils
{
    public class DoomBubblesHooks
    {
        public static void Load()
        {
            On.Terraria.Player.UpdateLifeRegen += PlayerOnUpdateLifeRegen;
            On.Terraria.Player.UpdateManaRegen += PlayerOnUpdateManaRegen;
            IL.Terraria.Player.Update += PlayerOnUpdate;
            On.Terraria.Main.DamageVar += (_, dmg, luck) => (int)Math.Round(dmg * (1 + luck / 20));
            On.Terraria.Main.DrawCursor += MainOnDrawCursor;
        }

        private static void MainOnDrawCursor(On.Terraria.Main.orig_DrawCursor orig, Vector2 bonus, bool smart)
        {
            var gameMenu = Main.gameMenu;
            if (gameMenu && Main.alreadyGrabbingSunOrMoon)
            {
                return;
            }
            
            var hasRainbowCursor = Main.LocalPlayer.hasRainbowCursor;
            
            Main.gameMenu = false;
            Main.LocalPlayer.hasRainbowCursor = true;
            
            orig(bonus, smart);

            Main.gameMenu = gameMenu;
            Main.LocalPlayer.hasRainbowCursor = hasRainbowCursor;
        }


        private static void PlayerOnUpdate(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdcI4(400)))
                return;

            c.Index -= 2;
            for (var i = 0; i < 7; i++)
            {
                c.Remove();
            }
        }
        
        private static void PlayerOnUpdateManaRegen(On.Terraria.Player.orig_UpdateManaRegen orig, Terraria.Player self)
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

        private static void PlayerOnUpdateLifeRegen(On.Terraria.Player.orig_UpdateLifeRegen orig, Terraria.Player self)
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
}