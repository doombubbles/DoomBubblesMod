using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    public class CardiopulmonaryMagnet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cardiopullmonary Magnet");
            Tooltip.SetDefault("Increases pickup range for life hearts");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.CelestialMagnet);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeMagnet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CelestialMagnet);
            recipe.AddIngredient(ItemID.HeartreachPotion, 5);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}