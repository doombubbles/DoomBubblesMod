using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    internal class FredericksGift : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frederick's Gift");
            Tooltip.SetDefault("42% reduced mana usage");
        }

        public override void SetDefaults()
        {
            item.value = 100000;
            item.width = 34;
            item.height = 22;
            item.rare = 2;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaCost -= .42f;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NaturesGift, 7);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}