using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class Rainbow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.extraUpdates = 2;
            projectile.scale = 1f;
            projectile.timeLeft = 600;
            projectile.magic = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.light = .3f;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var originX = (Main.projectileTexture[projectile.type].Width - projectile.width) * 0.5f +
                          projectile.width * 0.5f;
            var offsetX = 0;
            var offsetY = 0;
            var spriteEffects = SpriteEffects.None;
            var color25 = Lighting.GetColor((int) (projectile.position.X + projectile.width * 0.5) / 16,
                (int) ((projectile.position.Y + projectile.height * 0.5) / 16.0));

            var value11 = new Rectangle((int) Main.screenPosition.X - 500, (int) Main.screenPosition.Y - 500,
                Main.screenWidth + 1000, Main.screenHeight + 1000);
            if (projectile.getRect().Intersects(value11))
            {
                var value12 = new Vector2(projectile.position.X - Main.screenPosition.X + originX + offsetX,
                    projectile.position.Y - Main.screenPosition.Y + projectile.height / 2 + projectile.gfxOffY);
                var num152 = 100f;
                var scaleFactor = 3f;
                for (var num153 = 1; num153 <= (int) projectile.ai[1]; num153++)
                {
                    var value13 = Vector2.Normalize(projectile.velocity) * num153 * scaleFactor;
                    var alpha2 = projectile.GetAlpha(lightColor);
                    alpha2 *= (num152 - num153) / num152;
                    alpha2.A = 0;
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], value12 - value13, null, alpha2,
                        projectile.rotation, new Vector2(originX, projectile.height / 2 + offsetY), projectile.scale,
                        spriteEffects, 0f);
                }
            }

            return false;
        }

        public override void AI()
        {
            projectile.ai[0] = 0;

            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }

            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }

            var num55 = 100f;
            var num56 = 3f;

            projectile.ai[1] += num56;
            if (projectile.ai[1] > num55)
            {
                projectile.ai[1] = num55;
            }
        }

        public override void Kill(int timeLeft)
        {
            var num293 = Main.rand.Next(3, 7);
            for (var num294 = 0; num294 < num293; num294++)
            {
                var num295 = Dust.NewDust(projectile.Center - projectile.velocity / 2f, 0, 0, 63, 0f, 0f, 100,
                    DoomBubblesMod.rainbowColors[Main.rand.Next(0, 6)], 2.1f);
                var dust = Main.dust[num295];
                dust.velocity *= 2f;
                Main.dust[num295].noGravity = true;
            }

            base.Kill(timeLeft);
        }
    }
}