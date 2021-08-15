using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class TerraShard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.CrystalShard);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Shard");
        }
    }
}