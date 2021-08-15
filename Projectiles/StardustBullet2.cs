using System;
using DoomBubblesMod.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class StardustBullet2 : LunarBullet
    {
        public override int DustType => ModContent.DustType<Stardust229>();

        public override bool? CanHitNPC(NPC target)
        {
            if ((int) Math.Round(Projectile.ai[1]) == target.whoAmI)
            {
                return false;
            }

            return base.CanHitNPC(target);
        }
    }
}