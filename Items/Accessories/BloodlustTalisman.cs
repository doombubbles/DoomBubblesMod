﻿using IL.Terraria.ID;
using On.Terraria.GameContent.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.Accessories
{
    public class BloodlustTalisman : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodlust Talisman");
            Tooltip.SetDefault("Your lifesteal is uncapped");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
            item.rare = ItemRarityID.Red;
            item.accessory = true;
            item.value = Item.buyPrice(2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>().bloodlust = true;
        }
    }
}