using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    class TheSecondAmendment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Second Amendment");
            Tooltip.SetDefault("17.76% increased ranged firing rate");
        }

        public override void SetDefaults()
        {
            item.value = 100000;
            item.width = 30;
            item.height = 30;
            item.rare = 7;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>().fireRate += .1776f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.Feather);
            recipe.AddIngredient(ItemID.BlackInk);
            recipe.AddIngredient(ItemID.MusketBall, 1776);
            recipe.AddTile(TileID.LihzahrdAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
