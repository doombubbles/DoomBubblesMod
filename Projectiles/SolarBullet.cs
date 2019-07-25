using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace DoomBubblesMod.Projectiles
{
    public class SolarBullet : LunarBullet
    {
        public override int DustType => mod.DustType("Solar229");

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (!WorldUtils.Find(projectile.Center.ToTileCoordinates(),
                Searches.Chain(new Searches.Down(12), new Conditions.IsSolid()), out Point _))
            {
                damage = (int) (damage * 1.5f);
            }
            
            for (double theta = 0; theta < Math.PI * 2; theta += 2 * Math.PI / 5)
            {
                int dust = Dust.NewDust(projectile.Center,0, 0, DustType, (float) Math.Cos(theta), (float) Math.Sin(theta));
                Main.dust[dust].noGravity = true;
            }
            
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        
    }
}