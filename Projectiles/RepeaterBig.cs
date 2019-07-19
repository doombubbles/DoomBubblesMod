using Microsoft.Xna.Framework;
using Terraria;

namespace DoomBubblesMod.Projectiles
{
    public class RepeaterBig : Repeater
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.extraUpdates = 2;
            projectile.scale = 1f;
            projectile.timeLeft = 600;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }


        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += (int) (target.lifeMax * .06f);
        }
        
        

        public override void Kill(int timeLeft)
        {
            int num293 = Main.rand.Next(3, 7);
            for (int num294 = 0; num294 < num293; num294++)
            {
                int num295 = Dust.NewDust(projectile.Center - projectile.velocity / 2f, 0, 0, 63, 0f, 0f, 100, Color.Blue, 2.1f);
                Dust dust = Main.dust[num295];
                dust.velocity *= 2f;
                Main.dust[num295].noGravity = true;
            }
        }
    }
}