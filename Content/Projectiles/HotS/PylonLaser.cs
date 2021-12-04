namespace DoomBubblesMod.Content.Projectiles.HotS;

public class PylonLaser : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pylon Laser");
        ProjectileID.Sets.SentryShot[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 5;
        Projectile.height = 5;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.extraUpdates = 100;
        Projectile.timeLeft = 200;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.alpha = 255;
    }


    public override void AI()
    {
        var dust = Dust.NewDustPerfect(new Vector2(Projectile.position.X, Projectile.position.Y), 180);
        dust.noGravity = true;
    }
}