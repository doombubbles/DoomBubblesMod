using DoomBubblesMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Glove
{
    internal class RoseGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rose Gauntlet");
            Tooltip.SetDefault("5% increased symphonic damage");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 1);
            Item.width = 36;
            Item.height = 40;
            Item.rare = 1;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.SymphonicDamage(f => f + .05f);
        }

        public override void AddRecipes()
        {
            if (DoomBubblesMod.ThoriumMod != null)
            {
                addThoriumRecipe();
            }
        }

        private void addThoriumRecipe()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(DoomBubblesMod.ThoriumMod.Find<ModItem>("LeatherGlove"));
            recipe.AddIngredient(DoomBubblesMod.ThoriumMod.Find<ModItem>("Opal"), 7);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}