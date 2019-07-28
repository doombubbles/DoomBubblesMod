using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    class HeavenlyEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavenly Emblem");
            Tooltip.SetDefault("25% increased radiant damage");
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
            player.GetModPlayer<DoomBubblesPlayer>().customRadiantDamage += .25f;
        }

        public override void AddRecipes()
        {
            if (DoomBubblesMod.thoriumLoaded.HasValue && DoomBubblesMod.thoriumLoaded.Value)
            {
                ModRecipe recipe = new ModRecipe(mod);
                addThoriumRecipe(ref recipe);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
        
        public void addThoriumRecipe(ref ModRecipe recipe)
        {
            Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
            recipe.AddIngredient(thoriumMod.ItemType("ClericEmblem"));
            recipe.AddIngredient(thoriumMod.ItemType("CelestialFragment"), 5);
        }
    }
}
