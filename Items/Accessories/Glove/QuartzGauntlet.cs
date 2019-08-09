using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Glove
{
    class QuartzGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Quartz Gauntlet");
            Tooltip.SetDefault("7% increased crit chance");
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
            player.meleeCrit += 7;
            player.rangedCrit += 7;
            player.magicCrit += 7;
            player.thrownCrit += 7;
            player.GetModPlayer<DoomBubblesPlayer>().customRadiantCrit += 7;
            player.GetModPlayer<DoomBubblesPlayer>().customSymphonicCrit += 7;
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
            recipe.AddIngredient(ItemID.Diamond, 7);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
