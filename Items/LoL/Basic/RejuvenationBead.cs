using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Basic
{
    [AutoloadEquip(EquipType.Neck)]
    public class RejuvenationBead : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Slightly increased life regen");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 1, 50);
            item.width = 30;
            item.height = 26;
            item.rare = 1;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 1;
        }
    }
}