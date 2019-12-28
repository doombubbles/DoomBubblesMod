using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Basic
{
    class ClothArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cloth Armor");
            Tooltip.SetDefault("+2 Defense");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 3);
            item.width = 34;
            item.height = 22;
            item.rare = 1;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 2;
        }
    }
}
