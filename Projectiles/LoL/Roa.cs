using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class Roa : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.CultistBossFireBallClone);
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.magic = true;
        }

        public override void AI()
        {
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(SoundID.Item34, projectile.position);
            }
            else if (projectile.ai[1] == 1f && Main.netMode != 1)
            {
                int num13 = -1;
                float num14 = 2000f;
                for (int num15 = 0; num15 < Main.npc.Length; num15++)
                {
                    if (Main.npc[num15].active && Main.npc[num15].life > 0)
                    {
                        Vector2 center2 = Main.npc[num15].Center;
                        float num16 = Vector2.Distance(center2, projectile.Center);
                        if ((num16 < num14 || num13 == -1) && Collision.CanHit(projectile.Center, 1, 1, center2, 1, 1))
                        {
                            num14 = num16;
                            num13 = num15;
                        }
                    }
                }

                if (num14 < 20f)
                {
                    projectile.Kill();
                    return;
                }

                if (num13 != -1)
                {
                    projectile.ai[1] = 21f;
                    projectile.ai[0] = num13;
                    projectile.netUpdate = true;
                }
            }
            else if (projectile.ai[1] > 20f && projectile.ai[1] < 200f)
            {
                projectile.ai[1] += 1f;
                int num17 = (int) projectile.ai[0];
                if (!Main.npc[num17].active || Main.npc[num17].life <= 0)
                {
                    projectile.ai[1] = 1f;
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                else
                {
                    float num18 = projectile.velocity.ToRotation();
                    Vector2 vector2 = Main.npc[num17].Center - projectile.Center;
                    if (vector2.Length() < 20f)
                    {
                        projectile.Kill();
                        return;
                    }

                    float targetAngle2 = vector2.ToRotation();
                    if (vector2 == Vector2.Zero)
                    {
                        targetAngle2 = num18;
                    }

                    float num19 = num18.AngleLerp(targetAngle2, 0.01f);
                    projectile.velocity = new Vector2(projectile.velocity.Length(), 0f).RotatedBy(num19);
                }
            }

            if (projectile.ai[1] >= 1f && projectile.ai[1] < 20f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] == 20f)
                {
                    projectile.ai[1] = 1f;
                }
            }

            projectile.alpha -= 40;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }

            projectile.spriteDirection = projectile.direction;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }

            Lighting.AddLight(projectile.Center, 0.2f, 0.1f, 0.6f);
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] == 12f)
            {
                projectile.localAI[0] = 0f;
                for (int num20 = 0; num20 < 12; num20++)
                {
                    Vector2 value6 = Vector2.UnitX * (0f - (float) projectile.width) / 2f;
                    value6 += -Vector2.UnitY.RotatedBy((float) num20 * (float) Math.PI / 6f) * new Vector2(8f, 16f);
                    value6 = value6.RotatedBy(projectile.rotation - (float) Math.PI / 2f);
                    int num21 = Dust.NewDust(projectile.Center, 0, 0, 27, 0f, 0f, 160);
                    Main.dust[num21].scale = 1.1f;
                    Main.dust[num21].noGravity = true;
                    Main.dust[num21].position = projectile.Center + value6;
                    Main.dust[num21].velocity = projectile.velocity * 0.1f;
                    Main.dust[num21].velocity =
                        Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num21].position) *
                        1.25f;
                }
            }

            if (Main.rand.Next(4) == 0)
            {
                for (int num22 = 0; num22 < 1; num22++)
                {
                    Vector2 value7 = -Vector2.UnitX.RotatedByRandom(0.19634954631328583)
                        .RotatedBy(projectile.velocity.ToRotation());
                    int num23 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 100);
                    Main.dust[num23].velocity *= 0.1f;
                    Main.dust[num23].position = projectile.Center + value7 * projectile.width / 2f;
                    Main.dust[num23].fadeIn = 0.9f;
                }
            }

            if (Main.rand.Next(32) == 0)
            {
                for (int num24 = 0; num24 < 1; num24++)
                {
                    Vector2 value8 = -Vector2.UnitX.RotatedByRandom(0.39269909262657166)
                        .RotatedBy(projectile.velocity.ToRotation());
                    int num25 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 155,
                        default(Color), 0.8f);
                    Main.dust[num25].velocity *= 0.3f;
                    Main.dust[num25].position = projectile.Center + value8 * projectile.width / 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num25].fadeIn = 1.4f;
                    }
                }
            }

            if (Main.rand.Next(2) == 0)
            {
                for (int num26 = 0; num26 < 2; num26++)
                {
                    Vector2 value9 = -Vector2.UnitX.RotatedByRandom(0.78539818525314331)
                        .RotatedBy(projectile.velocity.ToRotation());
                    int num27 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, 0f, 0f, 0,
                        default(Color), 1.2f);
                    Main.dust[num27].velocity *= 0.3f;
                    Main.dust[num27].noGravity = true;
                    Main.dust[num27].position = projectile.Center + value9 * projectile.width / 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num27].fadeIn = 1.4f;
                    }
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            projectile.position = projectile.Center;
            projectile.width = (projectile.height = 176);
            projectile.Center = projectile.position;
            projectile.Damage();
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int num213 = 0; num213 < 4; num213++)
            {
                int num214 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num214].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) *
                                             (float) Main.rand.NextDouble() * projectile.width / 2f;
            }

            for (int num215 = 0; num215 < 20; num215++)
            {
                int num216 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, 27, 0f, 0f, 200, default(Color), 3.7f);
                Main.dust[num216].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) *
                                             (float) Main.rand.NextDouble() * projectile.width / 2f;
                Main.dust[num216].noGravity = true;
                Dust dust = Main.dust[num216];
                dust.velocity *= 3f;
                num216 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, 27, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num216].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) *
                                             (float) Main.rand.NextDouble() * projectile.width / 2f;
                dust = Main.dust[num216];
                dust.velocity *= 2f;
                Main.dust[num216].noGravity = true;
                Main.dust[num216].fadeIn = 2.5f;
            }

            for (int num217 = 0; num217 < 10; num217++)
            {
                int num218 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, 27, 0f, 0f, 0, default(Color), 2.7f);
                Main.dust[num218].position = projectile.Center +
                                             Vector2.UnitX.RotatedByRandom(3.1415927410125732)
                                                 .RotatedBy(projectile.velocity.ToRotation()) * projectile.width / 2f;
                Main.dust[num218].noGravity = true;
                Dust dust = Main.dust[num218];
                dust.velocity *= 3f;
            }

            for (int num219 = 0; num219 < 10; num219++)
            {
                int num220 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, 31, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num220].position = projectile.Center +
                                             Vector2.UnitX.RotatedByRandom(3.1415927410125732)
                                                 .RotatedBy(projectile.velocity.ToRotation()) * projectile.width / 2f;
                Main.dust[num220].noGravity = true;
                Dust dust = Main.dust[num220];
                dust.velocity *= 3f;
            }

            for (int num221 = 0; num221 < 2; num221++)
            {
                int num222 =
                    Gore.NewGore(
                        projectile.position + new Vector2((float) (projectile.width * Main.rand.Next(100)) / 100f,
                            (float) (projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f,
                        default(Vector2), Main.rand.Next(61, 64));
                Main.gore[num222].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) *
                                             (float) Main.rand.NextDouble() * projectile.width / 2f;
                Gore gore = Main.gore[num222];
                gore.velocity *= 0.3f;
                Gore gore33 = Main.gore[num222];
                gore33.velocity.X = gore33.velocity.X + (float) Main.rand.Next(-10, 11) * 0.05f;
                Gore gore34 = Main.gore[num222];
                gore34.velocity.Y = gore34.velocity.Y + (float) Main.rand.Next(-10, 11) * 0.05f;
            }
        }
    }
}