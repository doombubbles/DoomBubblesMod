namespace DoomBubblesMod.Projectiles
{
    public class MeleeDamage : Damage
    {
        public override void SetDefaults()
        {
            projectile.melee = true;
        }
    }
}