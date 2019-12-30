using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Basic
{
    [AutoloadEquip(EquipType.Back)]
    class NullMagicMantle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Null-Magic Mantle");
            Tooltip.SetDefault("Reduces damage taken by 2.5%");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 4, 50);
            item.width = 32;
            item.height = 34;
            item.rare = 1;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += .025f;
        }
    }
}
