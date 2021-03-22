using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    public class PalladiumVoodooDoll : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Voodoo Doll");
            Tooltip.SetDefault("Your life regenerates palladiumly");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.GuideVoodooDoll);
            item.rare += 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.onHitRegen = true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GuideVoodooDoll);
            recipe.AddIngredient(ItemID.TissueSample, 5);
            recipe.AddRecipeGroup("DoomBubblesMod:AnyPalladiumHelmet");
            recipe.AddIngredient(ItemID.PalladiumBreastplate);
            recipe.AddIngredient(ItemID.PalladiumLeggings);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}