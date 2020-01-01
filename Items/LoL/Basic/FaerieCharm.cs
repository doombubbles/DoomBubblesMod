using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Basic
{
    [AutoloadEquip(EquipType.Neck)]
    public class FaerieCharm : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slightly increased mana regen");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 1, 25);
            item.width = 34;
            item.height = 34;
            item.rare = 1;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaRegenBonus += 10;
        }
    }
}