using System;
using DoomBubblesMod.Common.GlobalProjectiles;
using DoomBubblesMod.Content.Items.Thanos;
using Terraria;

namespace DoomBubblesMod.Content.Projectiles.Thanos;

public class RealityBeam : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.penetrate = -1;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 60;
        Projectile.hide = true;
        Projectile.damage = 100;
    }

    public override void AI()
    {
        var player = Main.player[Projectile.owner];
        var gauntlet = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);

        HandleProjectiles(gauntlet);

        if (Math.Sqrt(Math.Pow(gauntlet.X - Projectile.position.X, 2) +
                      Math.Pow(gauntlet.Y - Projectile.position.Y, 2)) <
            30f)
        {
            Projectile.Kill();
            return;
        }

        for (var i = 0; i < 6; i++)
        {
            var dust = Main.dust[Dust.NewDust(new Vector2(Projectile.Center.X + Projectile.velocity.X * i / 6,
                    Projectile.Center.Y + Projectile.velocity.Y * i / 6), 0, 0, DustID.BubbleBurst_White, 0, 0, 0,
                InfinityGauntlet.RealityColor,
                1.5f)];
            dust.velocity *= .5f;
            dust.noGravity = true;
            dust.customData = "Reality Beam";
        }
        /*
        Vector2 vector = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
        if (player.direction != 1)
        {
            vector.X = player.bodyFrame.Width - vector.X;
        }
        if (player.gravDir != 1f)
        {
            vector.Y = player.bodyFrame.Height - vector.Y;
        }
        vector -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
        Vector2 goTowards = player.RotatedRelativePoint(player.position + vector) - player.velocity;
        */


        double dXPlayer = gauntlet.X - Projectile.Center.X;
        double dYPlayer = gauntlet.Y - Projectile.Center.Y;
        var theta = Math.Atan2(dYPlayer, dXPlayer);

        Projectile.velocity.X = (float) (40 * Math.Cos(theta));
        Projectile.velocity.Y = (float) (40 * Math.Sin(theta));
    }

    public void HandleProjectiles(Vector2 gauntlet)
    {
        foreach (var otherProjectile in Main.projectile)
        {
            if (otherProjectile.Distance(Projectile.Center) < 16f &&
                !otherProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().realityStoned &&
                otherProjectile.type != ProjectileType<RealityBeam>() &&
                (!otherProjectile.friendly || otherProjectile.hostile))
            {
                otherProjectile.friendly = true;
                otherProjectile.hostile = false;
                var mousePos = Main.MouseWorld;
                var theta2 = Math.Atan2(mousePos.Y - gauntlet.Y, mousePos.X - gauntlet.X);
                otherProjectile.Center = gauntlet;
                var speed = Math.Sqrt(Math.Pow(otherProjectile.velocity.X, 2) +
                                      Math.Pow(otherProjectile.velocity.Y, 2));
                otherProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().realityStoned = true;
                otherProjectile.velocity = new Vector2((float) (Math.Max(1.5 * speed, 5) * Math.Cos(theta2)),
                    (float) (Math.Max(1.5 * speed, 5) * Math.Sin(theta2)));
            }
        }
    }


    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.Confused, 60 * Main.rand.Next(5, 10));
        target.AddBuff(BuffID.Ichor, 60 * Main.rand.Next(5, 10));
        target.AddBuff(BuffID.Venom, 60 * Main.rand.Next(5, 10));
        target.AddBuff(BuffID.OnFire, 60 * Main.rand.Next(5, 10));
        target.AddBuff(BuffID.CursedInferno, 60 * Main.rand.Next(5, 10));
        target.AddBuff(BuffID.Frostburn, 60 * Main.rand.Next(5, 10));
        if (Main.player[Projectile.owner].gravControl2)
        {
            target.immune[Projectile.owner] = 1;
        }
        else
        {
            target.immune[Projectile.owner] = 7;
        }

        base.OnHitNPC(target, hit, damageDone);
    }
}