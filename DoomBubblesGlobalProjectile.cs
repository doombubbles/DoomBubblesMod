using System;
using System.Collections.Generic;
using DoomBubblesMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod
{
    internal class DoomBubblesGlobalProjectile : GlobalProjectile
    {
        public static List<int> newLocalNpcImmunity = new List<int>()
        {
            ProjectileID.LastPrismLaser, ProjectileID.ShadowBeamFriendly, ProjectileID.InfernoFriendlyBlast,
            ProjectileID.LostSoulFriendly, ProjectileID.FallingStar, ProjectileID.SolarWhipSword
        };

        public float db;

        public bool realityStone;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile projectile)
        {
            if (newLocalNpcImmunity.Contains(projectile.type))
            {
                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = 10;
            }

            if (projectile.type == ProjectileID.LastPrismLaser)
            {
                projectile.localNPCHitCooldown = 30;
            }

            if (projectile.ModProjectile != null && (projectile.ModProjectile.Name == "Nebulash" ||
                                                     projectile.ModProjectile.Name == "Nebudust"))
            {
                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = 20;
            }
        }

        public override bool PreAI(Projectile pProjectile)
        {
            if (pProjectile.friendly && Main.player[pProjectile.owner].GetModPlayer<DoomBubblesPlayer>().homing &&
                (pProjectile.aiStyle == 1 || pProjectile.aiStyle == 2 || pProjectile.aiStyle == 5 ||
                 pProjectile.aiStyle == 27))
            {
                if (pProjectile.type == ProjectileID.LunarFlare) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.NebulaBlaze1) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.NebulaBlaze2) return base.PreAI(pProjectile);
                if (pProjectile.type == ProjectileID.ChlorophyteBullet)
                    return base.PreAI(pProjectile); // don't want to do tracking x2
                if (pProjectile.type == ProjectileID.VortexBeaterRocket)
                    return base.PreAI(pProjectile); // don't want to do tracking x2
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

                var projSpeed = (float) Math.Sqrt(pProjectile.velocity.X * pProjectile.velocity.X +
                                                  pProjectile.velocity.Y * pProjectile.velocity.Y);
                var num139 = pProjectile.localAI[1];
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

                var initialX = pProjectile.position.X;
                var initialY = pProjectile.position.Y;
                var homingDistance = 300f;
                var flag4 = false;
                var num143 = 0;
                if (pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().db == 0f)
                {
                    for (var n = 0; n < 200; n++)
                    {
                        if (Main.npc[n].CanBeChasedBy(pProjectile) &&
                            (pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().db == 0f
                             || pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().db == n + 1))
                        {
                            var npcCenterX = Main.npc[n].position.X + Main.npc[n].width / 2;
                            var npcCenterY = Main.npc[n].position.Y + Main.npc[n].height / 2;
                            var distanceDifference =
                                Math.Abs(pProjectile.position.X + pProjectile.width / 2 - npcCenterX) +
                                Math.Abs(pProjectile.position.Y + pProjectile.height / 2 - npcCenterY);
                            if (distanceDifference < homingDistance && Collision.CanHit(
                                new Vector2(pProjectile.position.X + pProjectile.width / 2,
                                    pProjectile.position.Y + pProjectile.height / 2), 1, 1, Main.npc[n].position,
                                Main.npc[n].width, Main.npc[n].height))
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
                        pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().db = num143 + 1;
                    }

                    flag4 = false;
                }

                if (pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().db > 0f)
                {
                    var num148 = (int) (pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().db - 1f);
                    if (Main.npc[num148].active && Main.npc[num148].CanBeChasedBy(pProjectile, true) &&
                        !Main.npc[num148].dontTakeDamage)
                    {
                        var num149 = Main.npc[num148].position.X + Main.npc[num148].width / 2;
                        var num150 = Main.npc[num148].position.Y + Main.npc[num148].height / 2;
                        var num151 = Math.Abs(pProjectile.position.X + pProjectile.width / 2 - num149) +
                                     Math.Abs(pProjectile.position.Y + pProjectile.height / 2 - num150);
                        if (num151 < 1000f)
                        {
                            flag4 = true;
                            initialX = Main.npc[num148].position.X + Main.npc[num148].width / 2;
                            initialY = Main.npc[num148].position.Y + Main.npc[num148].height / 2;
                        }
                    }
                    else
                    {
                        pProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().db = 0f;
                    }
                }

                if (!pProjectile.friendly)
                {
                    flag4 = false;
                }

                if (flag4)
                {
                    var num152 = num139;
                    var vector13 = new Vector2(pProjectile.position.X + pProjectile.width * 0.5f,
                        pProjectile.position.Y + pProjectile.height * 0.5f);
                    var num153 = initialX - vector13.X;
                    var num154 = initialY - vector13.Y;
                    var num155 = (float) Math.Sqrt(num153 * num153 + num154 * num154);
                    num155 = num152 / num155;
                    num153 *= num155;
                    num154 *= num155;
                    var num156 = 8;
                    pProjectile.velocity.X = (pProjectile.velocity.X * (num156 - 1) + num153) / num156;
                    pProjectile.velocity.Y = (pProjectile.velocity.Y * (num156 - 1) + num154) / num156;
                }

                return base.PreAI(pProjectile);
            }

            return base.PreAI(pProjectile);
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback,
            ref bool crit,
            ref int hitDirection)
        {
            if (projectile.owner != 255 &&
                Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>().luminiteBulletBonus &&
                projectile.type == ProjectileID.MoonlordBullet)
            {
                projectile.damage = (int) (projectile.damage * 1.1f);
            }

            base.ModifyHitNPC(projectile, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(projectile, target, damage, knockback, crit);
            if (projectile.type == ProjectileID.LastPrismLaser)
            {
                target.immune[projectile.owner] = 0;
                projectile.localNPCImmunity[target.whoAmI] = projectile.localNPCHitCooldown;
            }
        }

        public override void Kill(Projectile projectile, int timeLeft)
        {
            base.Kill(projectile, timeLeft);
            if (Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>().crystalBulletBonus &&
                projectile.owner == Main.myPlayer)
            {
                int type;
                if (projectile.type == ProjectileID.CrystalBullet)
                {
                    type = ProjectileID.CrystalShard;
                }
                else if (projectile.type == ModContent.ProjectileType<TerraBullet>())
                {
                    type = ModContent.ProjectileType<TerraShard>();
                }
                else
                {
                    return;
                }

                for (var index = 0; index < 3; ++index)
                {
                    var SpeedX =
                        (float) (-(double) projectile.velocity.X * Main.rand.Next(40, 70) * 0.00999999977648258 +
                                 Main.rand.Next(-20, 21) * 0.400000005960464);
                    var SpeedY =
                        (float) (-(double) projectile.velocity.Y * Main.rand.Next(40, 70) * 0.00999999977648258 +
                                 Main.rand.Next(-20, 21) * 0.400000005960464);
                    Projectile.NewProjectile(new ProjectileSource_ProjectileParent(projectile), projectile.position.X + SpeedX, projectile.position.Y + SpeedY, SpeedX,
                        SpeedY, type, (int) (projectile.damage * 0.5), 0.0f, projectile.owner);
                }
            }
        }
    }
}