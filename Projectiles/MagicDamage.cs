using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class MagicDamage : Damage
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
        }
    }
}