using System;
using ElementalDamage.Content.DamageClasses;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class HolyBullet : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.CrystalBullet);
        AIType = ProjectileID.Bullet;
        Projectile.DamageType = GetInstance<RangedHoly>();
    }


    public override Color? GetAlpha(Color lightColor)
    {
        return Projectile.alpha < 200
            ? new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 0)
            : Color.Transparent;
    }

    public override void AI()
    {
        if (Projectile.alpha > 0)
            Projectile.alpha -= 15;
        if (Projectile.alpha < 0)
            Projectile.alpha = 0;
    }

    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (var num558 = 0; num558 < 10; num558++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink,
                Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 150, default, 1.2f);
        }

        for (var num559 = 0; num559 < 3; num559++)
        {
            Gore.NewGore(new EntitySource_Parent(Projectile), Projectile.position,
                new Vector2(Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18));
        }

        if (Projectile.owner == Main.myPlayer)
        {
            var position = new Vector2(Projectile.position.X + Main.rand.Next(-400, 400),
                Projectile.position.Y - Main.rand.Next(600, 900));
            var speedX = Projectile.position.X + Projectile.width / 2f - position.X;
            var speedY = Projectile.position.Y + Projectile.height / 2f - position.Y;
            var speedLength = (float) Math.Sqrt(speedX * speedX + speedY * speedY);
            speedLength = 22 / speedLength;
            speedX *= speedLength;
            speedY *= speedLength;
            var damage = Projectile.damage / 2;

            var proj = Projectile.NewProjectileDirect(new EntitySource_Parent(Projectile), position,
                new Vector2(speedX, speedY), ProjectileID.HallowStar, damage, Projectile.knockBack, Projectile.owner);

            proj.ai[1] = Projectile.position.Y;
            proj.ai[0] = 1f;
        }
    }
}