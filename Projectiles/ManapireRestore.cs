using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class ManapireRestore : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Manapire Restore");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.VampireHeal);
            projectile.aiStyle = -1;
        }

        public override void AI()
        {
            int healTarget = (int) projectile.ai[0];
            float speed = 4f;
            Vector2 vector31 = new Vector2(projectile.position.X + (float) projectile.width * 0.5f,
                projectile.position.Y + (float) projectile.height * 0.5f);
            float dX = Main.player[healTarget].Center.X - vector31.X;
            float dY = Main.player[healTarget].Center.Y - vector31.Y;
            float distance = (float) Math.Sqrt(dX * dX + dY * dY);
            if (distance < 50f &&
                projectile.position.X < Main.player[healTarget].position.X + (float) Main.player[healTarget].width &&
                projectile.position.X + (float) projectile.width > Main.player[healTarget].position.X &&
                projectile.position.Y < Main.player[healTarget].position.Y + (float) Main.player[healTarget].height &&
                projectile.position.Y + (float) projectile.height > Main.player[healTarget].position.Y)
            {
                if (projectile.owner == Main.myPlayer)
                {
                    Player player = Main.player[healTarget];
                    int amount = (int) projectile.ai[1];

                    if (player.statMana < player.statManaMax2)
                    {
                        //mana restore
                        amount = (int) (amount * 0.25f);
                        amount = Math.Min(amount, player.statManaMax2 - player.statMana);
                        player.ManaEffect(amount);
                        player.statMana += amount;
                    }
                    else if (!Main.player[Main.myPlayer].moonLeech && player.statLife < player.statLifeMax2 &&
                             !(player.lifeSteal <= 0f))
                    {
                        //life restore
                        amount = (int) (amount * 0.075f);
                        if (amount != 0)
                        {
                            player.lifeSteal -= amount;
                            player.HealEffect(amount, broadcast: false);
                            player.statLife += amount;
                            if (Main.player[healTarget].statLife > Main.player[healTarget].statLifeMax2)
                            {
                                Main.player[healTarget].statLife = Main.player[healTarget].statLifeMax2;
                            }

                            NetMessage.SendData(66, -1, -1, null, healTarget, amount);
                        }
                    }
                }

                projectile.Kill();
            }

            distance = speed / distance;
            dX *= distance;
            dY *= distance;
            projectile.velocity.X = (projectile.velocity.X * 15f + dX) / 16f;
            projectile.velocity.Y = (projectile.velocity.Y * 15f + dY) / 16f;

            int num4;
            for (int num500 = 0; num500 < 3; num500 = num4 + 1)
            {
                float num501 = projectile.velocity.X * 0.334f * (float) num500;
                float num502 = (0f - projectile.velocity.Y * 0.334f) * (float) num500;
                int num503 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, mod.DustType("ManapireDust2"), 0f, 0f, 100, default(Color), 1.1f);
                Main.dust[num503].noGravity = true;
                Dust dust3 = Main.dust[num503];
                dust3.velocity *= 0f;
                Dust dust70 = Main.dust[num503];
                dust70.position.X = dust70.position.X - num501;
                Dust dust71 = Main.dust[num503];
                dust71.position.Y = dust71.position.Y - num502;
                num4 = num500;
            }
        }
    }
}