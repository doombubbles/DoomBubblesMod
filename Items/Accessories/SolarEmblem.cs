using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    class SolarEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Emblem");
            Tooltip.SetDefault("25% increased melee damage");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.width = 28;
            item.height = 28;
            item.rare = 10;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += .25f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WarriorEmblem);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
