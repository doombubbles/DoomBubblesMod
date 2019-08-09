using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    
    public class RainbowMachineGun : ModProjectile
    {
        
        
        
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 6;
            DisplayName.SetDefault("Rainbow Machine Gun");
        }

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.magic = true;
            projectile.ignoreWater = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            
            Texture2D texture2D14 = Main.projectileTexture[projectile.type];
            int num192 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y16 = num192 * projectile.frame;
            Vector2 vector27 = (projectile.position + new Vector2(projectile.width, projectile.height) / 2f +
                                Vector2.UnitY * projectile.gfxOffY - Main.screenPosition).Floor();
            float scale5 = 1f;

            spriteBatch.Draw(texture2D14, vector27,
                new Microsoft.Xna.Framework.Rectangle(0, y16, texture2D14.Width, num192), projectile.GetAlpha(lightColor),
                projectile.rotation, new Vector2((float) texture2D14.Width / 2f, (float) num192 / 2f), projectile.scale,
                spriteEffects, 0f);
            
            
            spriteBatch.Draw(mod.GetTexture("Projectiles/RainbowMachineGun_Glow"), vector27,
                new Microsoft.Xna.Framework.Rectangle(0, y16, texture2D14.Width, num192),
                new Microsoft.Xna.Framework.Color(255, 255, 255, 0) * scale5, projectile.rotation,
                new Vector2((float) texture2D14.Width / 2f, (float) num192 / 2f), projectile.scale, spriteEffects,
                0f);
            
            return false;
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            float num = (float) Math.PI / 2f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter);
            projectile.ai[0] += 1f;
            int num2 = 0;
            int num3 = 24;
            int num4 = 6;
            if (projectile.ai[0] >= 40f)
            {
                num2++;
            }

            if (projectile.ai[0] >= 80f)
            {
                num2++;
            }

            if (projectile.ai[0] >= 120f)
            {
                num2++;
            }
            
            if (projectile.ai[0] >= 180f)
            {
                num3 = 22;
            }
            
            projectile.ai[1] += 1f;
            bool flag = false;
            if (projectile.ai[1] >= (float) (num3 - num4 * num2))
            {
                projectile.ai[1] = 0f;
                flag = true;
            }

            projectile.frameCounter += 1 + num2;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= 6)
                {
                    projectile.frame = 0;
                }
            }

            if (projectile.soundDelay <= 0)
            {
                projectile.soundDelay = num3 - num4 * num2;
                if (Math.Abs(projectile.ai[0] - 1f) > .0001f)
                {
                    Main.PlaySound(SoundID.Item91, projectile.position);
                }
            }

            
            if (projectile.ai[1] == 1f && projectile.ai[0] != 1f)
            {
                Vector2 spinningpoint = Vector2.UnitX * 24f;
                spinningpoint = spinningpoint.RotatedBy(projectile.rotation - (float) Math.PI / 2f);
                Vector2 value = projectile.Center + spinningpoint;
                for (int i = 0; i < 2; i++)
                {
                    int num5 = Dust.NewDust(value - Vector2.One * 8f, 16, 16, 63, projectile.velocity.X / 2f,
                        projectile.velocity.Y / 2f,
                        100, DoomBubblesMod.rainbowColors[Main.rand.Next(0, 6)]);
                    Main.dust[num5].velocity *= 0.66f;
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].scale = 1.4f;
                }
            }

            if (flag && Main.myPlayer == projectile.owner)
            {
                if (player.channel && player.CheckMana(player.inventory[player.selectedItem], -1, pay: true) &&
                    !player.noItems && !player.CCed)
                {
                    float scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    Vector2 vector2 = vector;
                    Vector2 value2 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - vector2;
                    if (player.gravDir == -1f)
                    {
                        value2.Y = (float) (Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector2.Y;
                    }

                    Vector2 vector3 = Vector2.Normalize(value2);
                    if (float.IsNaN(vector3.X) || float.IsNaN(vector3.Y))
                    {
                        vector3 = -Vector2.UnitY;
                    }

                    vector3 *= scaleFactor;
                    if (vector3.X != projectile.velocity.X || vector3.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }

                    projectile.velocity = vector3;
                    float scaleFactor2 = 14f;
                    int num7 = 7;
                    for (int j = 0; j < 2; j++)
                    {
                        vector2 = projectile.Center + new Vector2(Main.rand.Next(-num7, num7 + 1),
                                      Main.rand.Next(-num7, num7 + 1));
                        Vector2 spinningpoint2 = Vector2.Normalize(projectile.velocity) * scaleFactor2;
                        spinningpoint2 =
                            spinningpoint2.RotatedBy(
                                Main.rand.NextDouble() * 0.19634954631328583 - 0.098174773156642914);
                        if (float.IsNaN(spinningpoint2.X) || float.IsNaN(spinningpoint2.Y))
                        {
                            spinningpoint2 = -Vector2.UnitY;
                        }

                        int proj = Projectile.NewProjectile(vector2.X, vector2.Y, spinningpoint2.X, spinningpoint2.Y,
                            mod.ProjectileType("Rainbow"), projectile.damage, projectile.knockBack, projectile.owner);
                        Main.projectile[proj].netUpdate = true;
                    }
                }
                else
                {
                    projectile.Kill();
                }
            }

            projectile.position = player.RotatedRelativePoint(player.MountedCenter) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float) Math.Atan2(projectile.velocity.Y * (float) projectile.direction,
                projectile.velocity.X * (float) projectile.direction);
            /*for (int num46 = 0; num46 < 2; num46++)
            {
                Dust obj = Main.dust[Dust.NewDust(projectile.position + projectile.velocity * 2f, projectile.width, projectile.height, 6, 0f, 0f, 100, Color.Transparent, 2f)];
                obj.noGravity = true;
                obj.velocity *= 2f;
                obj.velocity += projectile.localAI[0].ToRotationVector2();
                obj.fadeIn = 1.5f;
            }*/

            /*
            for (int num48 = 0; (float) num48 < num47; num48++)
            {
                if (Main.rand.Next(4) == 0)
                {
                    Vector2 position = projectile.position + projectile.velocity +
                                       projectile.velocity * ((float) num48 / num47);
                    Dust obj2 = Main.dust[
                        Dust.NewDust(position, projectile.width, projectile.height, 6, 0f, 0f, 100, Color.Transparent)];
                    obj2.noGravity = true;
                    obj2.fadeIn = 0.5f;
                    obj2.velocity += projectile.localAI[0].ToRotationVector2();
                    obj2.noLight = true;
                }
            }
            */
        }

        public override void PostAI()
        {
            Player player = Main.player[projectile.owner];
            Item item = player.inventory[player.selectedItem];
            int time = (int)((float)item.useTime / PlayerHooks.TotalUseTimeMultiplier(player, item));
            projectile.ai[0] += 20f / time - 1;
            projectile.ai[1] += 20f / time - 1;
        }
    }
}