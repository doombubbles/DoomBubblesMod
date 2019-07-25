using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public class NebulaBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nebula Bullet");
            Tooltip.SetDefault("Teleports to enemies if close");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.MoonlordBullet);
            item.shoot = mod.ProjectileType("NebulaBullet");
            item.shootSpeed = 1.5f;
            item.damage = 17;
        }
    }
}