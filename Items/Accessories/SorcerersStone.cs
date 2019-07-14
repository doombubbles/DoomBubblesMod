using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    class SorcerersStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sorcerer's Stone");
            Tooltip.SetDefault("Your mana regenerates more freely\n"
                                + "(Mana Regen Potion effect)");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.PhilosophersStone);
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaRegenBuff = true;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PhilosophersStone);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
            
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(this);
            recipe2.AddTile(TileID.AlchemyTable);
            recipe2.SetResult(ItemID.PhilosophersStone);
            recipe2.AddRecipe();
        }
    }
}
