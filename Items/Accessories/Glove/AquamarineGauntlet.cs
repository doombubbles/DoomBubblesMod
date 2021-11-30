﻿using DoomBubblesMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Glove
{
    internal class AquamarineGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquamarine Gauntlet");
            Tooltip.SetDefault("5% increased radiant damage");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 1);
            Item.width = 36;
            Item.height = 40;
            Item.rare = 1;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.RadiantDamage(f => f + .05f);
        }

        public override void AddRecipes()
        {
            if (DoomBubblesMod.ThoriumMod != null)
            {
                addThoriumRecipe();
            }
        }

        private void addThoriumRecipe()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(DoomBubblesMod.ThoriumMod.Find<ModItem>("LeatherGlove"));
            recipe.AddIngredient(DoomBubblesMod.ThoriumMod.Find<ModItem>("Pearl"), 7);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}