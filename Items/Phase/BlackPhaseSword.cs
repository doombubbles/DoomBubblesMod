using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Phase
{
    public class BlackPhaseSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Phasesword");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BluePhasesaber);
            Item.damage = 81;
            Item.scale = 1.3f;
            Item.rare = ItemRarityID.Lime;
        }

        public override void AddRecipes()
        {
            if (DoomBubblesMod.thoriumMod != null)
            {
                AddThoriumRecipe();
            }
        }

        private void AddThoriumRecipe()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(DoomBubblesMod.thoriumMod.Find<ModItem>("BlackPhasesaber"));
            recipe.AddIngredient(ItemID.Ectoplasm, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}