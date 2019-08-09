using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Glove
{
    class JadeGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jade Gauntlet");
            Tooltip.SetDefault("7% increased ranged damage");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 1, 0 ,0);
            item.width = 36;
            item.height = 40;
            item.rare = 1;
            item.accessory = true;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += .07f;
        }

        public override void AddRecipes()
        {
            if (DoomBubblesMod.thoriumLoaded.HasValue && DoomBubblesMod.thoriumLoaded.Value)
            {
                addThoriumRecipe();
            }
        }

        private void addThoriumRecipe()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("LeatherGlove"));
            recipe.AddIngredient(ItemID.Emerald, 7);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
