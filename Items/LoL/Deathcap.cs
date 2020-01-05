using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod.Items.LoL
{
    class Deathcap : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rabadon's Deathcap");
            Tooltip.SetDefault("12% increased magic damage\n" +
                               "Magic damage increases are 25% more effective");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 36);
            item.width = 44;
            item.height = 40;
            item.rare = 8;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += .12f;
            player.GetModPlayer<LoLPlayer>().rabadon = true;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("NeedlesslyLargeRod"));
            recipe.AddIngredient(mod.ItemType("NeedlesslyLargeRod"));
            recipe.AddIngredient(ItemID.GoldCoin, 11);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
