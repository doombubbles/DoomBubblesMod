using DoomBubblesMod.Utils;
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
            Tooltip.SetDefault("42% reduced mana usage\n" +
                               "Mana Flower's effects overriden");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.value = 100000;
            Item.width = 34;
            Item.height = 22;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaCost -= .42f;
            player.GetDoomBubblesPlayer().noManaFlower = true;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.NaturesGift, 7);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}