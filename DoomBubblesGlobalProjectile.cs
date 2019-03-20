using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod
{

    class DoomBubblesGlobalProjectile: GlobalProjectile
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public List<int> cleaving = new List<int> { };

        public float db;

        public bool realityStone;

        public bool ak47;

        public override bool PreAI(Projectile pProjectile)
        {
            if (pProjectile.friendly && Main.player[pProjectile.owner].GetModPlayer<DoomBubblesPlayer>(mod).homing && (pProjectile.aiStyle == 1 || pProjectile.aiStyle == 2 || pProjectile.aiStyle == 5 || pProjectile.aiStyle == 27))
            {
                if (pProjectile.type == ProjectileID.LunarFlare) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.NebulaBlaze1) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.NebulaBlaze2) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.ChlorophyteBullet) return base.PreAI(pProjectile); // don't want to do tracking x2
                if (pProjectile.type == ProjectileID.VortexBeaterRocket) return base.PreAI(pProjectile); // don't want to do tracking x2
                if (pProjectile.type == ProjectileID.PygmySpear) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.MiniRetinaLaser) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.ElectrosphereMissile) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.Meteor1) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.Meteor2) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.Meteor3) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.MoonlordArrow) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.MoonlordArrowTrail) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.MiniSharkron) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.Phantasm) return base.PreAI(pProjectile);
                /*
                if (pProjectile.type == 13) return base.PreAI(pProjectile);
                if (pProjectile.type == 73) return base.PreAI(pProjectile);
                if (pProjectile.type == 74) return base.PreAI(pProjectile);
                if (pProjectile.type >= 230 && pProjectile.type <= 235) return base.PreAI(pProjectile);
                if (pProjectile.type == 315) return base.PreAI(pProjectile);
                if (pProjectile.type == 322) return base.PreAI(pProjectile);
                if (pProjectile.type == 331) return base.PreAI(pProjectile);
                if (pProjectile.type == 332) return base.PreAI(pProjectile);
                if (pProjectile.type == 372) return base.PreAI(pProjectile);
                if (pProjectile.type == 396) return base.PreAI(pProjectile);
                if (pProjectile.type == 403) return base.PreAI(pProjectile);
                if (pProjectile.type == 322) return base.PreAI(pProjectile);
                if (pProjectile.type == 446) return base.PreAI(pProjectile);
                if (pProjectile.name == "Hook") return base.PreAI(pProjectile);
                */

                if (pProjectile.aiStyle == 5 && pProjectile.timeLeft > 300)
                {
                    pProjectile.timeLeft = 300;
                    pProjectile.penetrate = 10;
                }
                
                float projSpeed = (float)Math.Sqrt((double)(pProjectile.velocity.X * pProjectile.velocity.X + pProjectile.velocity.Y * pProjectile.velocity.Y));
                float num139 = pProjectile.localAI[1];
                if (num139 == 0f)
                {
                    pProjectile.localAI[1] = projSpeed;
                    num139 = projSpeed;
                }
                if (pProjectile.alpha > 0)
                {
                    pProjectile.alpha -= 25;
                }
                if (pProjectile.alpha < 0)
                {
                    pProjectile.alpha = 0;
                }
                float initialX = pProjectile.position.X;
                float initialY = pProjectile.position.Y;
                float homingDistance = 300f;
                bool flag4 = false;
                int num143 = 0;
                if (pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>(mod).db == 0f)
                {
                    for (int n = 0; n < 200; n++)
                    {
                        if (Main.npc[n].CanBeChasedBy(pProjectile, false) && (pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>(mod).db == 0f 
                                                                                   || pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>(mod).db == (float)(n + 1)))
                        {
                            float npcCenterX = Main.npc[n].position.X + (float)(Main.npc[n].width / 2);
                            float npcCenterY = Main.npc[n].position.Y + (float)(Main.npc[n].height / 2);
                            float distanceDifference = Math.Abs(pProjectile.position.X + (float)(pProjectile.width / 2) - npcCenterX) + Math.Abs(pProjectile.position.Y + (float)(pProjectile.height / 2) - npcCenterY);
                            if (distanceDifference < homingDistance && Collision.CanHit(new Vector2(pProjectile.position.X + (float)(pProjectile.width / 2), pProjectile.position.Y + (float)(pProjectile.height / 2)), 1, 1, Main.npc[n].position, Main.npc[n].width, Main.npc[n].height))
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
                        pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>(mod).db = (float)(num143 + 1);
                    }
                    flag4 = false;
                }
                if (pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>(mod).db > 0f)
                {
                    int num148 = (int)(pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>(mod).db - 1f);
                    if (Main.npc[num148].active && Main.npc[num148].CanBeChasedBy(pProjectile, true) && !Main.npc[num148].dontTakeDamage)
                    {
                        float num149 = Main.npc[num148].position.X + (float)(Main.npc[num148].width / 2);
                        float num150 = Main.npc[num148].position.Y + (float)(Main.npc[num148].height / 2);
                        float num151 = Math.Abs(pProjectile.position.X + (float)(pProjectile.width / 2) - num149) + Math.Abs(pProjectile.position.Y + (float)(pProjectile.height / 2) - num150);
                        if (num151 < 1000f)
                        {
                            flag4 = true;
                            initialX = Main.npc[num148].position.X + (float)(Main.npc[num148].width / 2);
                            initialY = Main.npc[num148].position.Y + (float)(Main.npc[num148].height / 2);
                        }
                    }
                    else
                    {
                        pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>(mod).db = 0f;
                    }
                }
                if (!pProjectile.friendly)
                {
                    flag4 = false;
                }
                if (flag4)
                {
                    float num152 = num139;
                    Vector2 vector13 = new Vector2(pProjectile.position.X + (float)pProjectile.width * 0.5f, pProjectile.position.Y + (float)pProjectile.height * 0.5f);
                    float num153 = initialX - vector13.X;
                    float num154 = initialY - vector13.Y;
                    float num155 = (float)Math.Sqrt((double)(num153 * num153 + num154 * num154));
                    num155 = num152 / num155;
                    num153 *= num155;
                    num154 *= num155;
                    int num156 = 8;
                    pProjectile.velocity.X = (pProjectile.velocity.X * (float)(num156 - 1) + num153) / (float)num156;
                    pProjectile.velocity.Y = (pProjectile.velocity.Y * (float)(num156 - 1) + num154) / (float)num156;
                }
                return base.PreAI(pProjectile);
            }
            else return base.PreAI(pProjectile);
        }


        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            if (projectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().ak47)
            {
                Player player = Main.player[projectile.owner];
                if (crit)
                {
                    player.GetModPlayer<DoomBubblesPlayer>().critCombo++;
                    damage = (int) (damage / 2f) * (Math.Min(player.GetModPlayer<DoomBubblesPlayer>().critCombo, 9) + 1);
                }
                else
                {
                    player.GetModPlayer<DoomBubblesPlayer>().critCombo = 0;
                }
            }
            base.ModifyHitNPC(projectile, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}
