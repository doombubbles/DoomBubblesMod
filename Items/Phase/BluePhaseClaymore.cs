using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Phase
{
    public class BluePhaseClaymore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Phaseclaymore");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.BluePhasesaber);
            item.damage = 161;
            item.scale = 1.45f;
            item.rare = 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BluePhaseSword"));
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}