using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Basic
{
    [AutoloadEquip(EquipType.Back)]
    class CloakOfAgility : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cloak of Agility");
            Tooltip.SetDefault("5% increased critical strike chance");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 8);
            item.width = 34;
            item.height = 22;
            item.rare = 1;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeCrit += 5;
            player.rangedCrit += 5;
            player.magicCrit += 5;
            player.thrownCrit += 5;
            player.GetModPlayer<DoomBubblesPlayer>().customRadiantCrit += 5;
            player.GetModPlayer<DoomBubblesPlayer>().customSymphonicCrit += 5;
        }
    }
}
