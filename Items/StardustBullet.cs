using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public class StadustBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stardust Bullet");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.MoonlordBullet);
            item.shoot = mod.ProjectileType("StardustBullet");
            item.shootSpeed = 1.5f;
            item.damage = 17;
        }
    }
}