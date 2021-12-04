namespace DoomBubblesMod.Content.Projectiles.Magic;

public class MagicDamage : Damage
{
    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Magic;
    }
}