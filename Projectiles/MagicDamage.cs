namespace DoomBubblesMod.Projectiles
{
    public class MagicDamage : Damage
    {
        public override void SetDefaults()
        {
            projectile.magic = true;
        }
    }
}