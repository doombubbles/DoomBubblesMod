using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace DoomBubblesMod.Projectiles
{
    public class MidnightBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Blast");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            projectile.timeLeft = 120;
        }

        public override void AI()
        {
            
            projectile.ai[0] = 0;
            if (projectile.alpha <= 0)
            {
                for (int index1 = 0; index1 < 3; ++index1)
                {
                    int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 107, 0.0f, 0.0f, 0, new Color(0,0,0), 1f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].velocity *= 0.3f;
                    Main.dust[index2].noLight = true;
                }
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 55;
                projectile.scale = 1.3f;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                    float num = 16f;
                    for (int index1 = 0; (double) index1 < (double) num; ++index1)
                    {
                        Vector2 vector2 = (Vector2.UnitX * 0.0f + -Vector2.UnitY.RotatedBy((double) index1 * (6.28318548202515 / (double) num), new Vector2()) * new Vector2(1f, 4f)).RotatedBy((double) projectile.velocity.ToRotation(), new Vector2());
                        int index2 = Dust.NewDust(projectile.Center, 0, 0, 61, 0.0f, 0.0f, 0, new Color(), 1f);
                        Main.dust[index2].scale = 1.5f;
                        Main.dust[index2].noLight = true;
                        Main.dust[index2].noGravity = true;
                        Main.dust[index2].position = projectile.Center + vector2;
                        Main.dust[index2].velocity = Main.dust[index2].velocity * 4f + projectile.velocity * 0.3f;
                    }
                }
            }
        }


        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if ((double) Vector2.Distance(projHitbox.Center.ToVector2(), targetHitbox.Center.ToVector2()) > 500.0 || !Collision.CanHitLine(projHitbox.Center.ToVector2(), 0, 0, targetHitbox.Center.ToVector2(), 0, 0))
                return false;
            return base.Colliding(projHitbox, targetHitbox);

        }


        public override void Kill(int timeLeft)
        {
            projectile.position = projectile.Center;
            projectile.width = projectile.height = 160;
            projectile.Center = projectile.position;
            projectile.maxPenetrate = -1;
            projectile.penetrate = -1;
            projectile.Damage();
            Main.PlaySound(SoundID.Item14, projectile.position);
            Vector2 Position = projectile.Center + Vector2.One * -20f;
            int Width = 40;
            int Height = Width;
            for (int index1 = 0; index1 < 4; ++index1)
            {
                int index2 = Dust.NewDust(Position, Width, Height, 107, 0.0f, 0.0f, 100, new Color(), .75f);
                Main.dust[index2].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.14159274101257) * (float) Main.rand.NextDouble() * (float) Width / 2f;
            }
            for (int index1 = 0; index1 < 20; ++index1)
            {
                int index2 = Dust.NewDust(Position, Width, Height, 107, 0.0f, 0.0f, 200, new Color(), 1.5f);
                Main.dust[index2].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.14159274101257) * (float) Main.rand.NextDouble() * (float) Width / 2f;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].noLight = true;
                Main.dust[index2].velocity *= 1.5f;
                Main.dust[index2].velocity += projectile.DirectionTo(Main.dust[index2].position) * (float) (2.0 + (double) Main.rand.NextFloat() * 2.0);
                int index3 = Dust.NewDust(Position, Width, Height, 61, 0.0f, 0.0f, 100, new Color(), 1.5f);
                Main.dust[index3].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.14159274101257) * (float) Main.rand.NextDouble() * (float) Width / 2f;
                Main.dust[index3].velocity *= 1.5f;
                Main.dust[index3].noGravity = true;
                Main.dust[index3].fadeIn = 1f;
                Main.dust[index3].noLight = true;
                Main.dust[index3].velocity += projectile.DirectionTo(Main.dust[index3].position) * 4f;
            }
            for (int index1 = 0; index1 < 20; ++index1)
            {
                int index2 = Dust.NewDust(Position, Width, Height, 107, 0.0f, 0.0f, 0, new Color(), 1.3f);
                Main.dust[index2].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.14159274101257).RotatedBy((double) projectile.velocity.ToRotation(), new Vector2()) * (float) Width / 2f;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].noLight = true;
                Main.dust[index2].velocity *= 1.5f;
                Main.dust[index2].velocity += projectile.DirectionTo(Main.dust[index2].position) * 1f;
            }
            for (int index1 = 0; index1 < 70; ++index1)
            {
                int index2 = Dust.NewDust(Position, Width, Height, 107, 0.0f, 0.0f, 0, new Color(), .75f);
                Main.dust[index2].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.14159274101257).RotatedBy((double) projectile.velocity.ToRotation(), new Vector2()) * (float) Width / 2f;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 1.5f;
                Main.dust[index2].velocity += projectile.DirectionTo(Main.dust[index2].position) * 1.5f;
            }
        }
    }
    
}