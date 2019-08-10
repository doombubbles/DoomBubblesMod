using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace DoomBubblesMod.Projectiles
{
    public abstract class LunarBullet : ModProjectile
    {
        public abstract int DustType { get; }

        private readonly float NebulaDistance = 100f;
        
        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.CloneDefaults(ProjectileID.MoonlordBullet);
            projectile.timeLeft = 200;
            projectile.penetrate = 1;
        }

        public override void AI()
        {
            projectile.ai[0] = 0;
            float num117 = projectile.velocity.Length();
            if (projectile.alpha > 0)
            {
                projectile.alpha -= (byte)((double)num117 * 0.5);
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            Rectangle hitbox = projectile.Hitbox;
            hitbox.Offset((int)projectile.velocity.X, (int)projectile.velocity.Y);
            bool flag2 = false;
            for (int num118 = 0; num118 < 200; num118++)
            {
                NPC nPC = Main.npc[num118];
                if (nPC.active && !nPC.dontTakeDamage && nPC.immune[projectile.owner] == 0 && projectile.localNPCImmunity[num118] == 0 && nPC.Hitbox.Intersects(hitbox) && !nPC.friendly)
                {
                    flag2 = true;
                    break;
                }
            }
            if (flag2)
            {
                int num119 = Main.rand.Next(15, 31);
                for (int num120 = 0; num120 < num119; num120++)
                {
                    int num121 = Dust.NewDust(projectile.Center, 0, 0, DustType, 0f, 0f, 100, default(Color), 0.8f);
                    Main.dust[num121].velocity *= 1.6f;
                    Dust dust12 = Main.dust[num121];
                    dust12.velocity.Y = dust12.velocity.Y - 1f;
                    Main.dust[num121].velocity += projectile.velocity;
                    Main.dust[num121].noGravity = true;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            
            Texture2D texture2D3 = Main.projectileTexture[projectile.type];
            int num134 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y12 = num134 * projectile.frame;
            Rectangle rectangle = new Rectangle(0, y12, texture2D3.Width, num134);
            Vector2 origin2 = rectangle.Size() / 2f;

            
            
            origin2.Y = 2f;
            

            int num135 = 8;
            int num136 = 2;
            int num137 = 1;
            float value3 = 1f;
            float num138 = 0f;

            
            num135 = 5;
            num136 = 1;
            value3 = 1f;
            

            for (int num139 = num137;
                (num136 > 0 && num139 < num135) || (num136 < 0 && num139 > num135);
                num139 += num136)
            {
                Color color26 = lightColor;
                float num142 = num135 - num139;
                if (num136 < 0)
                {
                    num142 = num137 - num139;
                }

                color26 *= num142 / ((float) ProjectileID.Sets.TrailCacheLength[projectile.type] * 1.5f);
                Vector2 value4 = projectile.oldPos[num139];
                float num143 = projectile.rotation;
                SpriteEffects effects = spriteEffects;

                spriteBatch.Draw(texture2D3,
                    value4 + projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
                    rectangle, color26,
                    num143 + projectile.rotation * num138 * (float) (num139 - 1) *
                    (0f - (float) spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt()), origin2,
                    MathHelper.Lerp(projectile.scale, value3, (float) num139 / 15f), effects, 0f);
            }

            Color color28 = projectile.GetAlpha(lightColor);
            spriteBatch.Draw(texture2D3, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
                rectangle, color28, projectile.rotation, origin2, projectile.scale, spriteEffects, 0f);

            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            int num576 = Main.rand.Next(2, 5);
            int num534;
            for (int num577 = 0; num577 < num576; num577 = num534 + 1)
            {
                int num578 = Dust.NewDust(projectile.Center, 0, 0, DustType, 0f, 0f, 100);
                Dust dust = Main.dust[num578];
                dust.velocity *= 1.6f;
                Dust dust58 = Main.dust[num578];
                dust58.velocity.Y = dust58.velocity.Y - 1f;
                dust = Main.dust[num578];
                dust.position -= Vector2.One * 4f;
                Main.dust[num578].position = Vector2.Lerp(Main.dust[num578].position, projectile.Center, 0.5f);
                Main.dust[num578].noGravity = true;
                num534 = num577;
            }
            return base.OnTileCollide(oldVelocity);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 100) * projectile.Opacity;
        }

        public void SolarEffect(ref int damage)
        {
            if (!WorldUtils.Find(projectile.Center.ToTileCoordinates(),
                Searches.Chain(new Searches.Down(12), new Conditions.IsSolid()), out Point _))
            {
                damage = (int) (damage * 1.5f);
            
                for (double theta = 0; theta < Math.PI * 2; theta += 2 * Math.PI / 5)
                {
                    int dust = Dust.NewDust(projectile.Center,0, 0, mod.DustType("Solar229"), (float) Math.Cos(theta), (float) Math.Sin(theta));
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 2f;
                }
            }
        }

        public void NebulaEffect()
        {
            int targetI = -1;
            for (var i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC != null && nPC.active && nPC.immune[projectile.owner] == 0 && (!projectile.usesLocalNPCImmunity || projectile.localNPCImmunity[i] == 0) && nPC.CanBeChasedBy(projectile) && nPC.Hitbox.Distance(projectile.Center) < NebulaDistance)
                {
                    targetI = nPC.whoAmI;
                    break;
                }
            }

            if (targetI != -1)
            {
                NPC target = Main.npc[targetI];
                Vector2 newPos = new Vector2();

                int dX = projectile.width / 4;
                int dY = projectile.height / 4;
                
                bool leftLeft = target.Hitbox.Left < projectile.Center.X;
                bool rightRight = target.Hitbox.Right > projectile.Center.X;
                bool upUp = target.Hitbox.Top < projectile.Center.Y;
                bool downDown = target.Hitbox.Bottom > projectile.Center.Y;

                if (leftLeft && rightRight)
                {
                    newPos.X = projectile.Center.X;
                } else if (leftLeft)
                {
                    newPos.X = target.Hitbox.Right - dX;
                } else if (rightRight)
                {
                    newPos.X = target.Hitbox.Left + dX;
                }
                if (upUp && downDown)
                {
                    newPos.Y = projectile.Center.Y;
                } else if (upUp)
                {
                    newPos.Y = target.Hitbox.Bottom - dY;
                } else if (downDown)
                {
                    newPos.Y = target.Hitbox.Top + dY;
                }

                double newDX = projectile.velocity.Length() * Math.Cos((newPos - projectile.Center).ToRotation());
                double newDY = projectile.velocity.Length() * Math.Sin((newPos - projectile.Center).ToRotation());

                for (double theta = 0; theta < Math.PI * 2; theta += Math.PI / 12)
                {
                    int dust1 = Dust.NewDust(
                        projectile.Center + new Vector2((float) (10 * Math.Cos(theta)), (float) (10 * Math.Sin(theta))),
                        0, 0, mod.DustType("Nebula229"), (float) Math.Cos(theta + Math.PI), (float) Math.Sin(theta + Math.PI));
                    Main.dust[dust1].noGravity = true;
                    
                    int dust2 = Dust.NewDust(newPos,0, 0, mod.DustType("Nebula229"), (float) Math.Cos(theta), (float) Math.Sin(theta));
                    Main.dust[dust2].noGravity = true;
                }

                projectile.Center = newPos;
                projectile.velocity = new Vector2((float) newDX, (float) newDY);
            }
            
            
        }

        public void VortexEffect (NPC target)
        {
            Vector2 pos = Main.player[projectile.owner].Center +
                          new Vector2(Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-30, 30));
            Vector2 v = 7f * (target.Center - pos).ToRotation().ToRotationVector2();
            
            int proj = Projectile.NewProjectile(pos, v, mod.ProjectileType("VortexBullet2"), 
                projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
            Main.projectile[proj].netUpdate = true;
        }

        public void StardustEffect(NPC target)
        {
            Vector2 newPos = projectile.position;
            newPos += projectile.oldVelocity / 5f;
            while (target.Hitbox.Contains(newPos.ToPoint()))
            {
                newPos += projectile.oldVelocity / 5f;
            }
            
            Projectile.NewProjectile(newPos, projectile.oldVelocity.RotatedByRandom(Math.PI / 15) / 2,
                mod.ProjectileType("StardustBullet2"), projectile.damage / 2, projectile.knockBack / 2, projectile.owner, 0, target.whoAmI);
            Projectile.NewProjectile(newPos, projectile.oldVelocity.RotatedByRandom(Math.PI / 15) / 2,
                mod.ProjectileType("StardustBullet2"), projectile.damage / 2, projectile.knockBack / 2, projectile.owner, 0, target.whoAmI);
        }
        
    }
}