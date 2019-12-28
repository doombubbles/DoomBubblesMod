using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Basic
{
    public class SapphireCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases maximum mana by 25");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 3, 50);
            item.width = 22;
            item.height = 22;
            item.rare = 1;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 25;
        }
    }
}