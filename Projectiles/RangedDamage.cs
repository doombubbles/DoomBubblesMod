namespace DoomBubblesMod.Projectiles
{
    public class RangedDamage : Damage
    {
        public override void SetDefaults()
        {
            projectile.ranged = true;
        }
    }
}