using DoomBubblesMod.Utils;
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
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.PhilosophersStone);
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>().sStone = true;
        }


        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PhilosophersStone);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();

            var recipe2 = CreateRecipe();
            recipe2.AddIngredient(this);
            recipe2.AddTile(TileID.AlchemyTable);
            recipe2.ReplaceResult(ItemID.PhilosophersStone);
            recipe2.Register();
        }
    }
}