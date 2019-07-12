using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Phase
{
    public class BluePhaseSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Phasesword");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.BluePhasesaber);
            item.damage = 81;
            item.scale = 1.3f;
            item.rare = 7;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BluePhaseblade);
            recipe.AddIngredient(ItemID.Ectoplasm, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}