using System;
using Terraria;

namespace DoomBubblesMod.Projectiles
{
    public class StardustBullet2 : LunarBullet
    {
        public override int DustType => mod.DustType("Stardust229");

        public override bool? CanHitNPC(NPC target)
        {
            if ((int) Math.Round(projectile.ai[1]) == target.whoAmI)
            {
                return false;
            }

            return base.CanHitNPC(target);
        }
    }
}