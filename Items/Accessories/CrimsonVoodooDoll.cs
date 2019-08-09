using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.Accessories
{
    public class CrimsonVoodooDoll : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Voodoo Doll");
            Tooltip.SetDefault("Your life regenerates crimsonly");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.GuideVoodooDoll);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.crimsonRegen = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GuideVoodooDoll);
            recipe.AddIngredient(ItemID.TissueSample, 5);
            recipe.AddIngredient(ItemID.CrimsonHelmet);
            recipe.AddIngredient(ItemID.CrimsonScalemail);
            recipe.AddIngredient(ItemID.CrimsonGreaves);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}