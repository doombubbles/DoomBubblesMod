using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DoomBubblesMod.UI;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using On.Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using RecipeGroup = Terraria.RecipeGroup;


namespace DoomBubblesMod
{
    public class DoomBubblesHooks
    {
        public static void Load()
        {
            Player.UpdateLifeRegen += PlayerOnUpdateLifeRegen;
            Player.UpdateManaRegen += PlayerOnUpdateManaRegen;
            IL.Terraria.Player.Update += PlayerOnUpdate;
            Main.DamageVar += (_, dmg, luck) => (int)Math.Round(dmg * (1 + luck / 20));
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
}