namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class RangedDamage : Damage
{
    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Ranged;
    }
}