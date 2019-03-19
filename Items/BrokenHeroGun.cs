using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public class BrokenHeroGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Hero Gun");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 26;
            item.maxStack = 99;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 8;
        }
    }
}