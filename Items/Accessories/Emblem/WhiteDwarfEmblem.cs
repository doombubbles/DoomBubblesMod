using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Emblem
{
    internal class WhiteDwarfEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Dwarf Emblem");
            Tooltip.SetDefault("20% increased throwing damage");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 5);
            item.width = 28;
            item.height = 28;
            item.rare = 10;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.thrownDamage += .2f;
        }

        public override void AddRecipes()
        {
            if (DoomBubblesMod.thoriumMod != null)
            {
                var recipe = new ModRecipe(mod);
                addThoriumRecipe(ref recipe);
                recipe.AddIngredient(ItemID.LunarBar, 5);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }

        public void addThoriumRecipe(ref ModRecipe recipe)
        {
            var thoriumMod = ModLoader.GetMod("ThoriumMod");
            recipe.AddIngredient(thoriumMod.ItemType("NinjaEmblem"));
        }
    }
}