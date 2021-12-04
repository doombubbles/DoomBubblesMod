using System;
using DoomBubblesMod.Common.Players;
using Terraria.DataStructures;

namespace DoomBubblesMod.Content.Projectiles.Summon;

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
        Main.projFrames[Projectile.type] = 4;
        Main.projPet[Projectile.type] = true;
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 72;
        Projectile.height = 38;
        Projectile.netImportant = true;
        Projectile.friendly = true;
        Projectile.minion = true;
        Projectile.minionSlots = 1f;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 18000;
        Projectile.ignoreWater = true;
        Projectile.penetrate = -1;
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
        var player = Main.player[Projectile.owner];
        var modPlayer = player.GetModPlayer<DoomBubblesPlayer>();
        if (player.dead)
        {
            modPlayer.vampireKnifeBat = false;
        }

        if (modPlayer.vampireKnifeBat)
        {
            // Make sure you are resetting this bool in ModPlayer.ResetEffects. See ExamplePlayer.ResetEffects
            Projectile.timeLeft = 2;
        }
    }

    private void HandleFrames()
    {
        Projectile.frameCounter++;
        if (Projectile.frameCounter >= 6)
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
            }
        }
    }

    public void Behavior()
    {
        var player = Main.player[Projectile.owner];
        var spacing = Projectile.width * spacingMult;
        for (var k = 0; k < 1000; k++)
        {
            var otherProj = Main.projectile[k];
            if (k != Projectile.whoAmI &&
                otherProj.active &&
                otherProj.owner == Projectile.owner &&
                otherProj.type == Projectile.type &&
                Math.Abs(Projectile.position.X - otherProj.position.X) +
                Math.Abs(Projectile.position.Y - otherProj.position.Y) <
                spacing)
            {
                if (Projectile.position.X < Main.projectile[k].position.X)
                {
                    Projectile.velocity.X -= idleAccel;
                }
                else
                {
                    Projectile.velocity.X += idleAccel;
                }

                if (Projectile.position.Y < Main.projectile[k].position.Y)
                {
                    Projectile.velocity.Y -= idleAccel;
                }
                else
                {
                    Projectile.velocity.Y += idleAccel;
                }
            }
        }

        var targetPos = Projectile.position;
        var targetDist = viewDist;
        var target = false;
        Projectile.tileCollide = true;
        if (player.HasMinionAttackTargetNPC)
        {
            var npc = Main.npc[player.MinionAttackTargetNPC];
            if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position,
                    npc.width, npc.height))
            {
                targetDist = Vector2.Distance(Projectile.Center, targetPos);
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
                    var distance = Vector2.Distance(npc.Center, Projectile.Center);
                    if ((distance < targetDist || !target) &&
                        Collision.CanHitLine(Projectile.position,
                            Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        target = true;
                    }
                }
            }
        }

        if (Vector2.Distance(player.Center, Projectile.Center) > (target ? 1000f : 500f))
        {
            Projectile.ai[0] = 1f;
            Projectile.netUpdate = true;
        }

        if (Projectile.ai[0] == 1f)
        {
            Projectile.tileCollide = false;
        }

        if (target && Projectile.ai[0] == 0f)
        {
            var direction = targetPos - Projectile.Center;
            if (direction.Length() > chaseDist)
            {
                direction.Normalize();
                Projectile.velocity = (Projectile.velocity * inertia + direction * chaseAccel) / (inertia + 1);
            }
            else
            {
                Projectile.velocity *= (float) Math.Pow(0.97, 40.0 / inertia);
            }
        }
        else
        {
            if (!Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1))
            {
                Projectile.ai[0] = 1f;
            }

            var speed = 6f;
            if (Projectile.ai[0] == 1f)
            {
                speed = 15f;
            }

            var center = Projectile.Center;
            var direction = player.Center - center;
            Projectile.netUpdate = true;
            var num = 1;
            for (var k = 0; k < Projectile.whoAmI; k++)
            {
                if (Main.projectile[k].active &&
                    Main.projectile[k].owner == Projectile.owner &&
                    Main.projectile[k].type == Projectile.type)
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

            if (distanceTo < 100f &&
                Projectile.ai[0] == 1f &&
                !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.ai[0] = 0f;
                Projectile.netUpdate = true;
            }

            if (distanceTo > 2000f)
            {
                Projectile.Center = player.Center;
            }

            if (distanceTo > 48f)
            {
                direction.Normalize();
                direction *= speed;
                var temp = inertia / 2f;
                Projectile.velocity = (Projectile.velocity * temp + direction) / (temp + 1);
            }
            else
            {
                Projectile.direction = Main.player[Projectile.owner].direction;
                Projectile.velocity *= (float) Math.Pow(0.9, 40.0 / inertia);
            }
        }

        Projectile.rotation = Projectile.velocity.X * 0.05f;

        if (Projectile.velocity.X > 0f)
        {
            Projectile.spriteDirection = Projectile.direction = -1;
        }
        else if (Projectile.velocity.X < 0f)
        {
            Projectile.spriteDirection = Projectile.direction = 1;
        }

        if (Main.myPlayer == Projectile.owner &&
            player.itemAnimation == player.itemAnimationMax - 1 &&
            Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type ==
            ItemID.VampireKnives)
        {
            var x = Projectile.Center.X;
            var y = Projectile.Center.Y;
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
                    var proj = Projectile.NewProjectile(new ProjectileSource_ProjectileParent(Projectile),
                        Projectile.Center, perturbedSpeed,
                        ProjectileID.VampireKnife, Projectile.damage, Projectile.knockBack, player.whoAmI);
                    Main.projectile[proj].netUpdate = true;
                }
            }
            else
            {
                var proj = Projectile.NewProjectile(new ProjectileSource_ProjectileParent(Projectile),
                    Projectile.Center, speed, ProjectileID.VampireKnife,
                    Projectile.damage,
                    Projectile.knockBack, Projectile.owner);
                Main.projectile[proj].netUpdate = true;
            }

            Projectile.netUpdate = true;
        }
    }
}