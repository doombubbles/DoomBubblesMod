using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class RangedDamage : Damage
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
        }
    }
}