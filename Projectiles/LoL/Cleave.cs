using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class Cleave : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cleave");
        }

        public override void SetDefaults()
        {
            projectile.width = 150;
            projectile.height = 150;
            projectile.light = 0.5f;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.timeLeft = 420;
            projectile.hide = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            var player = Main.player[projectile.owner];
            Vector2 targetPos;
            if (projectile.ai[0] == 0f && Main.myPlayer == projectile.owner)
            {
                targetPos = Main.MouseWorld;
            }
            else
            {
                targetPos = new Vector2(projectile.Center.X - (player.Center.X - projectile.Center.X),
                    projectile.Center.Y - (player.Center.Y - projectile.Center.Y));
            }
            
            
            if (projectile.ai[1] == 0f)
            {
                if (projectile.ai[0] == 0f)
                {
                    projectile.Center = new Vector2(player.Center.X, player.Center.Y);
                }
                
                Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Crescent"));
                projectile.timeLeft = (int)(Main.player[projectile.owner].HeldItem.useTime * .75f);
                
                double theta = Math.Atan2(targetPos.Y - projectile.Center.Y , targetPos.X - projectile.Center.X);
                projectile.velocity = new Vector2((float) (7 * Math.Cos(theta)), (float) (7 * Math.Sin(theta)));

                if (projectile.ai[0] == 0f)
                {
                    projectile.velocity += player.velocity;
                }

                for (int i = -25; i <= 25; i++)
                {
                    double theta2 = theta - Math.PI / 2;
                    float dX2 = (float) (i * Math.Cos(theta2));
                    float dY2 = (float) (i * Math.Sin(theta2));
                    
                    Dust dust = Dust.NewDustPerfect(new Vector2(projectile.Center.X + dX2, projectile.Center.Y + dY2), 212, 
                        new Vector2((float) (20 * Math.Cos(theta - i / 100f)), (float) (20 * Math.Sin(theta - i / 100f))), 0, new Color(153, 51, 0), 1.5f);
                    dust.noGravity = true;
                    if (projectile.ai[0] == 0f)
                    {
                        dust.velocity += player.velocity;
                    }
                    
                    Dust dust2 = Dust.NewDustPerfect(new Vector2(projectile.Center.X + dX2, projectile.Center.Y + dY2), 212, 
                        new Vector2((float) (19 * Math.Cos(theta - i / 100f)), (float) (19 * Math.Sin(theta - i / 100f))), 0, new Color(153, 51, 0), 1.5f);
                    dust2.noGravity = true;
                    if (projectile.ai[0] == 0f)
                    {
                        dust2.velocity += player.velocity;
                    }
                    
                    Dust dust3 = Dust.NewDustPerfect(new Vector2(projectile.Center.X + dX2, projectile.Center.Y + dY2), 212, 
                        new Vector2((float) (15 * Math.Cos(theta - i / 100f)), (float) (15 * Math.Sin(theta - i / 100f))), 0, new Color(127, 51, 0), 1.5f);
                    dust3.noGravity = true;
                    if (projectile.ai[0] == 0f)
                    {
                        dust3.velocity += player.velocity;
                    }
                    
                    Dust dust4 = Dust.NewDustPerfect(new Vector2(projectile.Center.X + dX2, projectile.Center.Y + dY2), 212, 
                        new Vector2((float) (14 * Math.Cos(theta - i / 100f)), (float) (14 * Math.Sin(theta - i / 100f))), 0, new Color(127, 51, 0), 1.5f);
                    dust4.noGravity = true;
                    if (projectile.ai[0] == 0f)
                    {
                        dust4.velocity += player.velocity;
                    }
                }
                
                
            }
            projectile.ai[1] = 1f;

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        
        
    }
}