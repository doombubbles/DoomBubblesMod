using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    class SpectresCowl : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectre's Cowl");
            Tooltip.SetDefault("Increased maximum life by 25\n" +
                               "Reduces damage taken by 5%\n" +
                               "Taking damage gives you Regeneration");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 10);
            item.width = 28;
            item.height = 38;
            item.rare = 4;
            item.accessory = true;
            item.defense = 6;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().cowl = true;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ClothArmor"));
            recipe.AddIngredient(mod.ItemType("ClothArmor"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
