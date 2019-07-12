using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Phase
{
    public class PinkPhaseSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pink Phasesword");
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
            if (DoomBubblesMod.thoriumLoaded)
            {
                AddThoriumRecipe();
            }
        }

        private void AddThoriumRecipe()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("PinkPhaseblade"));
            recipe.AddIngredient(ItemID.Ectoplasm, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}