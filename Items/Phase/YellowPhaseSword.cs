using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Phase
{
    public class YellowPhaseSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yellow Phasesword");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.YellowPhasesaber);
            item.damage = 81;
            item.scale = 1.3f;
            item.rare = 7;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.YellowPhaseblade);
            recipe.AddIngredient(ItemID.Ectoplasm, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}