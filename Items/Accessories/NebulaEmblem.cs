using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    class NebulaEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nebula Emblem");
            Tooltip.SetDefault("25% increased magic damage");
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
            player.magicDamage += .25f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
