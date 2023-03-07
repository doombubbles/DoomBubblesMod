using ElementalDamage.Common.Types;
using ElementalDamage.Content.DamageClasses;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class TruePiercingBullet : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 8;
        Projectile.height = 8;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = ElementalDamageClass.Get<RangedDamageClass, Holy>();
        Projectile.penetrate = 3;
        Projectile.timeLeft = 600;
        Projectile.alpha = 255;
        Projectile.light = 0.5f;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        Projectile.extraUpdates = 1;
        Projectile.light = 0.3f;
        AIType = ProjectileID.Bullet;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 50;
    }

    public override void Kill(int timeLeft)
    {
        // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
        Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width,
            Projectile.height);
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
    }
}