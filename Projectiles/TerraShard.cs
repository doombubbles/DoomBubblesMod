using ElementalDamage.Elements;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class TerraShard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.CrystalShard);
            Projectile.DamageType = ModContent.GetInstance<RangedNature>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Shard");
        }
    }
}