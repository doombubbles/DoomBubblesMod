﻿using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Phase
{
    public class PurplePhaseClaymore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Purple Phaseclaymore");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.PurplePhasesaber);
            Item.damage = 161;
            Item.scale = 1.45f;
            Item.rare = ItemRarityID.Red;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PurplePhaseSword>());
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}