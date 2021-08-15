﻿using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Phase
{
    public class WhitePhaseClaymore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Phaseclaymore");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WhitePhasesaber);
            Item.damage = 161;
            Item.scale = 1.45f;
            Item.rare = ItemRarityID.Red;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WhitePhaseSword>());
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}