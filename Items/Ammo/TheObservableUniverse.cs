using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Ammo
{
    internal class TheObservableUniverse : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pocket Galaxy");
            Tooltip.SetDefault("An endless amount of stars");
        }

        public override void SetDefaults()
        {
            item.shoot = 12;
            item.width = 38;
            item.height = 38;
            item.ammo = AmmoID.FallenStar;
            item.value = Item.sellPrice(0, 2);
            item.ranged = true;
            item.rare = 3;
        }


        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 999);
            recipe.SetResult(this);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }
    }
}