using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    internal class SorcerersStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sorcerer's Stone");
            Tooltip.SetDefault("Your health and mana always regenerate as if you weren't moving");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.PhilosophersStone);
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>().sStone = true;
        }


        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PhilosophersStone);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();

            var recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(this);
            recipe2.AddTile(TileID.AlchemyTable);
            recipe2.SetResult(ItemID.PhilosophersStone);
            recipe2.AddRecipe();
        }
    }
}