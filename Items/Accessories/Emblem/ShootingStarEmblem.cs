﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Emblem
{
    class ShootingStarEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shooting Star Emblem");
            Tooltip.SetDefault("25% increased symphonic damage");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.width = 28;
            item.height = 28;
            item.rare = 10;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>().customSymphonicDamage += .25f;
        }

        public override void AddRecipes()
        {
            if (DoomBubblesMod.thoriumLoaded.HasValue && DoomBubblesMod.thoriumLoaded.Value)
            {
                ModRecipe recipe = new ModRecipe(mod);
                addThoriumRecipe(ref recipe);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
        
        public void addThoriumRecipe(ref ModRecipe recipe)
        {
            Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
            recipe.AddIngredient(thoriumMod.ItemType("BardEmblem"));
            recipe.AddIngredient(thoriumMod.ItemType("CometFragment"), 5);
        }
    }
}
