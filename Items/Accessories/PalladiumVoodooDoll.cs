using DoomBubblesMod.Utils;
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
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.GuideVoodooDoll);
            Item.rare += 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.onHitRegen = true;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GuideVoodooDoll);
            recipe.AddIngredient(ItemID.TissueSample, 5);
            recipe.AddRecipeGroup("DoomBubblesMod:AnyPalladiumHelmet");
            recipe.AddIngredient(ItemID.PalladiumBreastplate);
            recipe.AddIngredient(ItemID.PalladiumLeggings);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}