using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public class VortexBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vortex Bullet");
            Tooltip.SetDefault("Creates bullet echos on enemy hits");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.MoonlordBullet);
            item.shoot = mod.ProjectileType("VortexBullet");
            item.shootSpeed = 1.5f;
            item.damage = 17;
        }
    }
}