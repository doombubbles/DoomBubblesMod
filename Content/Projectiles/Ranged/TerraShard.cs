using ElementalDamage.Content.DamageClasses;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class TerraShard : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.CrystalShard);
        Projectile.DamageType = GetInstance<RangedNature>();
    }
}