using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class TerraBullet : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Terra Bullet");
        }

        public override void SetDefaults() {
            projectile.width = 8;               
            projectile.height = 8;              
            projectile.aiStyle = 1;             
            projectile.friendly = true;         
            projectile.hostile = false;         
            projectile.ranged = true;           
            projectile.penetrate = 3;           
            projectile.timeLeft = 600;          
            projectile.alpha = 255;             
            projectile.ignoreWater = true;      
            projectile.tileCollide = true;      
            projectile.extraUpdates = 1;   
            projectile.light = 0.3f;
            aiType = ProjectileID.Bullet;
        }

        public override bool PreAI()
        {
            if (projectile.numHits < 1)
            {
                float projSpeed = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
                float num139 = projectile.localAI[1];
                if (num139 == 0f)
                {
                    projectile.localAI[1] = projSpeed;
                    num139 = projSpeed;
                }
                if (projectile.alpha > 0)
                {
                    projectile.alpha -= 25;
                }
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                float initialX = projectile.position.X;
                float initialY = projectile.position.Y;
                float homingDistance = 300f;
                bool flag4 = false;
                int num143 = 0;
                if (projectile.ai[1] == 0f)
                {
                    for (int n = 0; n < 200; n++)
                    {
                        if (Main.npc[n].CanBeChasedBy(projectile, false) && (projectile.ai[1] == 0f 
                                                                             || projectile.ai[1] == (float)(n + 1)))
                        {
                            float npcCenterX = Main.npc[n].position.X + (float)(Main.npc[n].width / 2);
                            float npcCenterY = Main.npc[n].position.Y + (float)(Main.npc[n].height / 2);
                            float distanceDifference = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - npcCenterX) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - npcCenterY);
                            if (distanceDifference < homingDistance && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[n].position, Main.npc[n].width, Main.npc[n].height))
                            {
                                homingDistance = distanceDifference;
                                initialX = npcCenterX;
                                initialY = npcCenterY;
                                flag4 = true;
                                num143 = n;
                            }
                        }
                    }
                    if (flag4)
                    {
                        projectile.ai[1] = (float)(num143 + 1);
                    }
                    flag4 = false;
                }
                if (projectile.ai[1] > 0f)
                {
                    int num148 = (int)(projectile.ai[1] - 1f);
                    if (Main.npc[num148].active && Main.npc[num148].CanBeChasedBy(projectile, true) && !Main.npc[num148].dontTakeDamage)
                    {
                        float num149 = Main.npc[num148].position.X + (float)(Main.npc[num148].width / 2);
                        float num150 = Main.npc[num148].position.Y + (float)(Main.npc[num148].height / 2);
                        float num151 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num149) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num150);
                        if (num151 < 1000f)
                        {
                            flag4 = true;
                            initialX = Main.npc[num148].position.X + (float)(Main.npc[num148].width / 2);
                            initialY = Main.npc[num148].position.Y + (float)(Main.npc[num148].height / 2);
                        }
                    }
                    else
                    {
                        projectile.ai[1] = 0f;
                    }
                }
                if (!projectile.friendly)
                {
                    flag4 = false;
                }
                if (flag4)
                {
                    float num152 = num139;
                    Vector2 vector13 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num153 = initialX - vector13.X;
                    float num154 = initialY - vector13.Y;
                    float num155 = (float)Math.Sqrt((double)(num153 * num153 + num154 * num154));
                    num155 = num152 / num155;
                    num153 *= num155;
                    num154 *= num155;
                    int num156 = 8;
                    projectile.velocity.X = (projectile.velocity.X * (float)(num156 - 1) + num153) / (float)num156;
                    projectile.velocity.Y = (projectile.velocity.Y * (float)(num156 - 1) + num154) / (float)num156;
                }
            }
            
            return base.PreAI();
        }
        
        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
    }
}