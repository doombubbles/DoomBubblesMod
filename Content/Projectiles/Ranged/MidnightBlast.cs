using Terraria.Audio;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class MidnightBlast : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.alpha = 255;
        Projectile.ignoreWater = true;
        Projectile.extraUpdates = 1;
        Projectile.timeLeft = 120;
    }

    public override void AI()
    {
        Projectile.ai[0] = 0;
        if (Projectile.alpha <= 0)
        {
            for (var index1 = 0; index1 < 3; ++index1)
            {
                var index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TerraBlade,
                    0.0f, 0.0f,
                    0, new Color(0, 0, 0));
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 0.3f;
                Main.dust[index2].noLight = true;
            }
        }

        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= 55;
            Projectile.scale = 1.3f;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
                var num = 16f;
                for (var index1 = 0; index1 < (double) num; ++index1)
                {
                    var vector2 =
                        (Vector2.UnitX * 0.0f +
                         -Vector2.UnitY.RotatedBy(index1 * (6.28318548202515 / num)) *
                         new Vector2(1f, 4f)).RotatedBy(Projectile.velocity.ToRotation());
                    var index2 = Dust.NewDust(Projectile.Center, 0, 0, DustID.GreenTorch);
                    Main.dust[index2].scale = 1.5f;
                    Main.dust[index2].noLight = true;
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].position = Projectile.Center + vector2;
                    Main.dust[index2].velocity = Main.dust[index2].velocity * 4f + Projectile.velocity * 0.3f;
                }
            }
        }
    }


    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        if (Vector2.Distance(projHitbox.Center.ToVector2(), targetHitbox.Center.ToVector2()) > 500.0 ||
            !Collision.CanHitLine(projHitbox.Center.ToVector2(), 0, 0, targetHitbox.Center.ToVector2(), 0, 0))
        {
            return false;
        }

        return base.Colliding(projHitbox, targetHitbox);
    }


    public override void Kill(int timeLeft)
    {
        Projectile.position = Projectile.Center;
        Projectile.width = Projectile.height = 160;
        Projectile.Center = Projectile.position;
        Projectile.maxPenetrate = -1;
        Projectile.penetrate = -1;
        Projectile.Damage();
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        var position = Projectile.Center + Vector2.One * -20f;
        var width = 40;
        var height = width;
        for (var index1 = 0; index1 < 4; ++index1)
        {
            var index2 = Dust.NewDust(position, width, height, DustID.TerraBlade, 0.0f, 0.0f, 100, new Color(), .75f);
            Main.dust[index2].position = Projectile.Center +
                                         Vector2.UnitY.RotatedByRandom(3.14159274101257) *
                                         (float) Main.rand.NextDouble() *
                                         width /
                                         2f;
        }

        for (var index1 = 0; index1 < 20; ++index1)
        {
            var index2 = Dust.NewDust(position, width, height, DustID.TerraBlade, 0.0f, 0.0f, 200, new Color(), 1.5f);
            Main.dust[index2].position = Projectile.Center +
                                         Vector2.UnitY.RotatedByRandom(3.14159274101257) *
                                         (float) Main.rand.NextDouble() *
                                         width /
                                         2f;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].noLight = true;
            Main.dust[index2].velocity *= 1.5f;
            Main.dust[index2].velocity += Projectile.DirectionTo(Main.dust[index2].position) *
                                          (float) (2.0 + Main.rand.NextFloat() * 2.0);
            var index3 = Dust.NewDust(position, width, height, DustID.GreenTorch, 0.0f, 0.0f, 100, new Color(), 1.5f);
            Main.dust[index3].position = Projectile.Center +
                                         Vector2.UnitY.RotatedByRandom(3.14159274101257) *
                                         (float) Main.rand.NextDouble() *
                                         width /
                                         2f;
            Main.dust[index3].velocity *= 1.5f;
            Main.dust[index3].noGravity = true;
            Main.dust[index3].fadeIn = 1f;
            Main.dust[index3].noLight = true;
            Main.dust[index3].velocity += Projectile.DirectionTo(Main.dust[index3].position) * 4f;
        }

        for (var index1 = 0; index1 < 20; ++index1)
        {
            var index2 = Dust.NewDust(position, width, height, DustID.TerraBlade, 0.0f, 0.0f, 0, new Color(), 1.3f);
            Main.dust[index2].position = Projectile.Center +
                                         Vector2.UnitX.RotatedByRandom(3.14159274101257)
                                             .RotatedBy(Projectile.velocity.ToRotation()) *
                                         width /
                                         2f;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].noLight = true;
            Main.dust[index2].velocity *= 1.5f;
            Main.dust[index2].velocity += Projectile.DirectionTo(Main.dust[index2].position) * 1f;
        }

        for (var index1 = 0; index1 < 70; ++index1)
        {
            var index2 = Dust.NewDust(position, width, height, DustID.TerraBlade, 0.0f, 0.0f, 0, new Color(), .75f);
            Main.dust[index2].position = Projectile.Center +
                                         Vector2.UnitX.RotatedByRandom(3.14159274101257)
                                             .RotatedBy(Projectile.velocity.ToRotation()) *
                                         width /
                                         2f;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity *= 1.5f;
            Main.dust[index2].velocity += Projectile.DirectionTo(Main.dust[index2].position) * 1.5f;
        }
    }
}