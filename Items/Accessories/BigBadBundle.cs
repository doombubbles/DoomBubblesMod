using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Balloon)]
    class BigBadBundle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Big Bad Blessed Bundle of Balloons");
            Tooltip.SetDefault("Allows the holder to septuple jump\nReleases bees when damaged");
        }

        public override void SetDefaults()
        {
            item.value = 100000;
            item.width = 42;
            item.height = 42;
            item.rare = 6;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.doubleJumpBlizzard = true;
            player.doubleJumpCloud = true;
            player.doubleJumpFart = true;
            player.doubleJumpSandstorm = true;
            player.doubleJumpSail = true;
            player.doubleJumpUnicorn = true;
            player.bee = true;
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BundleofBalloons, 1);
            recipe.AddIngredient(ItemID.FartInABalloon, 1);
            recipe.AddIngredient(ItemID.HoneyBalloon, 1);
            recipe.AddIngredient(ItemID.SharkronBalloon, 1);
            recipe.AddIngredient(ItemID.BlessedApple, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
