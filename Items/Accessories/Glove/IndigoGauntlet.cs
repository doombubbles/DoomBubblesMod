using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Glove
{
    internal class IndigoGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Indigo Gauntlet");
            Tooltip.SetDefault("5% reduced damage taken");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 1);
            Item.width = 36;
            Item.height = 40;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += .05f;
        }

        public override void AddRecipes()
        {
            if (DoomBubblesMod.thoriumMod != null)
            {
                addThoriumRecipe();
            }
        }

        private void addThoriumRecipe()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(DoomBubblesMod.thoriumMod.Find<ModItem>("LeatherGlove"));
            recipe.AddIngredient(ItemID.Amethyst, 7);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}