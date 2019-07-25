using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class Echo2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Echo Blob");
        }

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.light = 0.3f;
            projectile.alpha = 100;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.magic = true;
            projectile.timeLeft = 60;
        }

        public override void AI()
        {
            projectile.rotation += 0.3f * (float)projectile.direction;
            if (Main.rand.Next(1, 3) == 1)
            {
                int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 62, projectile.velocity.X, projectile.velocity.Y);
            }
            projectile.velocity.Y += .2f;
        }
    }
}