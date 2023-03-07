using ElementalDamage.Common.Types;
using Terraria.Audio;
using Terraria.ID;

namespace DoomBubblesMod.Content.Projectiles.Ranged.AllTheBullets;

public abstract class AllTheBullet : ModProjectile
{
    protected abstract short SourceItem { get; }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Bullet);
        AIType = ProjectileID.Bullet;
        var source = new Item(SourceItem);
        if (source.DamageType is MultiDamageClass multiDamageClass)
        {
            Projectile.DamageType = ElementalDamageClass.Get(DamageClass.Ranged, multiDamageClass.ElementalDamageClass);
        }

        if (source.rare >= ItemRarityID.LightRed)
        {
            Projectile.extraUpdates++;
        }
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        return true;
    }
}