using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Basic
{
    public class RubyCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases maximum life by 15");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 4);
            item.width = 20;
            item.height = 18;
            item.rare = 1;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 15;
        }
    }
}