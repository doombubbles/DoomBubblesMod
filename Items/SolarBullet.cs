using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public class SolarBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Bullet");
            Tooltip.SetDefault("Deals bonus damage to airborne enemies");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.MoonlordBullet);
            item.shoot = mod.ProjectileType("SolarBullet");
            item.shootSpeed = 1.5f;
            item.damage = 17;
        }
    }
}