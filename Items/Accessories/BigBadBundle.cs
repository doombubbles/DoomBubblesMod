using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Balloon)]
    internal class BigBadBundle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Big Bad Blessed Bundle of Balloons");
            Tooltip.SetDefault("Allows the holder to septuple jump\nReleases bees when damaged");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.value = 100000;
            Item.width = 42;
            Item.height = 42;
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.hasJumpOption_Blizzard = true;
            player.hasJumpOption_Cloud = true;
            player.hasJumpOption_Fart = true;
            player.hasJumpOption_Sandstorm = true;
            player.hasJumpOption_Sail = true;
            player.hasJumpOption_Unicorn = true;
            player.honeyCombItem = Item;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BundleofBalloons);
            recipe.AddIngredient(ItemID.FartInABalloon);
            recipe.AddIngredient(ItemID.HoneyBalloon);
            recipe.AddIngredient(ItemID.SharkronBalloon);
            recipe.AddIngredient(ItemID.BlessedApple);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}