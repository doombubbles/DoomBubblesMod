using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class VampireKnifeBat : ModProjectile
    {
        protected float chaseAccel = 6f;
        protected float chaseDist = 200f;
        protected float idleAccel = 0.05f;
        protected float inertia = 40f;
        protected float spacingMult = .5f;
        protected float viewDist = 400f;

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.width = 72;
            projectile.height = 38;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.tileCollide = false;
            projectile.timeLeft = 18000;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override bool CanHitPlayer(Player target)
        {
            return false;
        }

        public override bool CanHitPvp(Player target)
        {
            return false;
        }

        public override void AI()
        {
            CheckActive();
            HandleFrames();
            Behavior();
        }

        public void CheckActive()
        {
            var player = Main.player[projectile.owner];
            var modPlayer = player.GetModPlayer<DoomBubblesPlayer>();
            if (player.dead)
            {
                modPlayer.vampireKnifeBat = false;
            }

            if (modPlayer.vampireKnifeBat)
            {
                // Make sure you are resetting this bool in ModPlayer.ResetEffects. See ExamplePlayer.ResetEffects
                projectile.timeLeft = 2;
            }
        }

        private void HandleFrames()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public void Behavior()
        {
            var player = Main.player[projectile.owner];
            var spacing = projectile.width * spacingMult;
            for (var k = 0; k < 1000; k++)
            {
                var otherProj = Main.projectile[k];
                if (k != projectile.whoAmI && otherProj.active && otherProj.owner == projectile.owner &&
                    otherProj.type == projectile.type && Math.Abs(projectile.position.X - otherProj.position.X) +
                    Math.Abs(projectile.position.Y - otherProj.position.Y) < spacing)
                {
                    if (projectile.position.X < Main.projectile[k].position.X)
                    {
                        projectile.velocity.X -= idleAccel;
                    }
                    else
                    {
                        projectile.velocity.X += idleAccel;
                    }

                    if (projectile.position.Y < Main.projectile[k].position.Y)
                    {
                        projectile.velocity.Y -= idleAccel;
                    }
                    else
                    {
                        projectile.velocity.Y += idleAccel;
                    }
                }
            }

            var targetPos = projectile.position;
            var targetDist = viewDist;
            var target = false;
            projectile.tileCollide = true;
            if (player.HasMinionAttackTargetNPC)
            {
                var npc = Main.npc[player.MinionAttackTargetNPC];
                if (Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position,
                    npc.width, npc.height))
                {
                    targetDist = Vector2.Distance(projectile.Center, targetPos);
                    targetPos = npc.Center;
                    target = true;
                }
            }
            else
            {
                for (var k = 0; k < 200; k++)
                {
                    var npc = Main.npc[k];
                    if (npc.CanBeChasedBy(this))
                    {
                        var distance = Vector2.Distance(npc.Center, projectile.Center);
                        if ((distance < targetDist || !target) && Collision.CanHitLine(projectile.position,
                            projectile.width, projectile.height, npc.position, npc.width, npc.height))
                        {
                            targetDist = distance;
                            targetPos = npc.Center;
                            target = true;
                        }
                    }
                }
            }

            if (Vector2.Distance(player.Center, projectile.Center) > (target ? 1000f : 500f))
            {
                projectile.ai[0] = 1f;
                projectile.netUpdate = true;
            }

            if (projectile.ai[0] == 1f)
            {
                projectile.tileCollide = false;
            }

            if (target && projectile.ai[0] == 0f)
            {
                var direction = targetPos - projectile.Center;
                if (direction.Length() > chaseDist)
                {
                    direction.Normalize();
                    projectile.velocity = (projectile.velocity * inertia + direction * chaseAccel) / (inertia + 1);
                }
                else
                {
                    projectile.velocity *= (float) Math.Pow(0.97, 40.0 / inertia);
                }
            }
            else
            {
                if (!Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1))
                {
                    projectile.ai[0] = 1f;
                }

                var speed = 6f;
                if (projectile.ai[0] == 1f)
                {
                    speed = 15f;
                }

                var center = projectile.Center;
                var direction = player.Center - center;
                projectile.netUpdate = true;
                var num = 1;
                for (var k = 0; k < projectile.whoAmI; k++)
                {
                    if (Main.projectile[k].active && Main.projectile[k].owner == projectile.owner &&
                        Main.projectile[k].type == projectile.type)
                    {
                        num++;
                    }
                }

                direction.X -= (10 + num * 40) * player.direction;
                direction.Y -= 70f;
                var distanceTo = direction.Length();
                if (distanceTo > 200f && speed < 9f)
                {
                    speed = 9f;
                }

                if (distanceTo < 100f && projectile.ai[0] == 1f &&
                    !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }

                if (distanceTo > 2000f)
                {
                    projectile.Center = player.Center;
                }

                if (distanceTo > 48f)
                {
                    direction.Normalize();
                    direction *= speed;
                    var temp = inertia / 2f;
                    projectile.velocity = (projectile.velocity * temp + direction) / (temp + 1);
                }
                else
                {
                    projectile.direction = Main.player[projectile.owner].direction;
                    projectile.velocity *= (float) Math.Pow(0.9, 40.0 / inertia);
                }
            }

            projectile.rotation = projectile.velocity.X * 0.05f;

            if (projectile.velocity.X > 0f)
            {
                projectile.spriteDirection = projectile.direction = -1;
            }
            else if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = projectile.direction = 1;
            }

            if (Main.myPlayer == projectile.owner && player.itemAnimation == player.itemAnimationMax - 1
                                                  && Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type ==
                                                  ItemID.VampireKnives)
            {
                var x = projectile.Center.X;
                var y = projectile.Center.Y;
                var theta = Math.Atan2(Main.MouseWorld.Y - y, Main.MouseWorld.X - x);
                var dX = 15f * Math.Cos(theta);
                var dY = 15f * Math.Sin(theta);
                var speed = new Vector2((float) dX, (float) dY);

                if (player.gravControl2)
                {
                    var numKnives = 4;
                    if (Main.rand.Next(2) == 0)
                    {
                        numKnives++;
                    }

                    if (Main.rand.Next(4) == 0)
                    {
                        numKnives++;
                    }

                    if (Main.rand.Next(8) == 0)
                    {
                        numKnives++;
                    }

                    if (Main.rand.Next(16) == 0)
                    {
                        numKnives++;
                    }

                    for (var i = 0; i < numKnives; i++)
                    {
                        var perturbedSpeed = speed.RotatedByRandom(MathHelper.ToRadians(35)); // 30 degree spread.
                        // If you want to randomize the speed to stagger the projectiles
                        // float scale = 1f - (Main.rand.NextFloat() * .3f);
                        // perturbedSpeed = perturbedSpeed * scale; 
                        var proj = Projectile.NewProjectile(projectile.Center, perturbedSpeed,
                            ProjectileID.VampireKnife, projectile.damage, projectile.knockBack, player.whoAmI);
                        Main.projectile[proj].netUpdate = true;
                    }
                }
                else
                {
                    var proj = Projectile.NewProjectile(projectile.Center, speed, ProjectileID.VampireKnife,
                        projectile.damage,
                        projectile.knockBack, projectile.owner);
                    Main.projectile[proj].netUpdate = true;
                }

                projectile.netUpdate = true;
            }
        }
    }
}