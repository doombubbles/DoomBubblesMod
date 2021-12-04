using System;
using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Common.GlobalProjectiles;

public class HomingGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;

    private float db;

    public override bool PreAI(Projectile projectile)
    {
        if (projectile.friendly &&
            Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>().homing &&
            (projectile.aiStyle == 1 ||
             projectile.aiStyle == 2 ||
             projectile.aiStyle == 5 ||
             projectile.aiStyle == 27))
        {
            if (projectile.type == ProjectileID.LunarFlare)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.NebulaBlaze1)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.NebulaBlaze2)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.ChlorophyteBullet)
            {
                return base.PreAI(projectile); // don't want to do tracking x2
            }

            if (projectile.type == ProjectileID.VortexBeaterRocket)
            {
                return base.PreAI(projectile); // don't want to do tracking x2
            }

            if (projectile.type == ProjectileID.PygmySpear)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.MiniRetinaLaser)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.ElectrosphereMissile)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.Meteor1)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.Meteor2)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.Meteor3)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.MoonlordArrow)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.MoonlordArrowTrail)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.MiniSharkron)
            {
                return base.PreAI(projectile);
            }

            if (projectile.type == ProjectileID.Phantasm)
            {
                return base.PreAI(projectile);
            }
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

            if (projectile.aiStyle == 5 && projectile.timeLeft > 300)
            {
                projectile.timeLeft = 300;
                projectile.penetrate = 10;
            }

            var projSpeed = (float) Math.Sqrt(projectile.velocity.X * projectile.velocity.X +
                                              projectile.velocity.Y * projectile.velocity.Y);
            var num139 = projectile.localAI[1];
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

            var initialX = projectile.position.X;
            var initialY = projectile.position.Y;
            var homingDistance = 300f;
            var flag4 = false;
            var num143 = 0;
            if (db == 0f)
            {
                for (var n = 0; n < 200; n++)
                {
                    if (Main.npc[n].CanBeChasedBy(projectile) &&
                        (db == 0f ||
                         db == n + 1))
                    {
                        var npcCenterX = Main.npc[n].position.X + Main.npc[n].width / 2;
                        var npcCenterY = Main.npc[n].position.Y + Main.npc[n].height / 2;
                        var distanceDifference =
                            Math.Abs(projectile.position.X + projectile.width / 2 - npcCenterX) +
                            Math.Abs(projectile.position.Y + projectile.height / 2 - npcCenterY);
                        if (distanceDifference < homingDistance &&
                            Collision.CanHit(
                                new Vector2(projectile.position.X + projectile.width / 2,
                                    projectile.position.Y + projectile.height / 2), 1, 1, Main.npc[n].position,
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
                    db = num143 + 1;
                }

                flag4 = false;
            }

            if (db > 0f)
            {
                var num148 = (int) (db - 1f);
                if (Main.npc[num148].active &&
                    Main.npc[num148].CanBeChasedBy(projectile, true) &&
                    !Main.npc[num148].dontTakeDamage)
                {
                    var num149 = Main.npc[num148].position.X + Main.npc[num148].width / 2;
                    var num150 = Main.npc[num148].position.Y + Main.npc[num148].height / 2;
                    var num151 = Math.Abs(projectile.position.X + projectile.width / 2 - num149) +
                                 Math.Abs(projectile.position.Y + projectile.height / 2 - num150);
                    if (num151 < 1000f)
                    {
                        flag4 = true;
                        initialX = Main.npc[num148].position.X + Main.npc[num148].width / 2;
                        initialY = Main.npc[num148].position.Y + Main.npc[num148].height / 2;
                    }
                }
                else
                {
                    db = 0f;
                }
            }

            if (!projectile.friendly)
            {
                flag4 = false;
            }

            if (flag4)
            {
                var num152 = num139;
                var vector13 = new Vector2(projectile.position.X + projectile.width * 0.5f,
                    projectile.position.Y + projectile.height * 0.5f);
                var num153 = initialX - vector13.X;
                var num154 = initialY - vector13.Y;
                var num155 = (float) Math.Sqrt(num153 * num153 + num154 * num154);
                num155 = num152 / num155;
                num153 *= num155;
                num154 *= num155;
                var num156 = 8;
                projectile.velocity.X = (projectile.velocity.X * (num156 - 1) + num153) / num156;
                projectile.velocity.Y = (projectile.velocity.Y * (num156 - 1) + num154) / num156;
            }

            return base.PreAI(projectile);
        }

        return base.PreAI(projectile);
    }
}