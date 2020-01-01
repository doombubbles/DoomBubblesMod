using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    class BrambleVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bramble Vest");
            Tooltip.SetDefault("Thorns effect");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 10);
            item.width = 40;
            item.height = 30;
            item.rare = 4;
            item.accessory = true;
            item.defense = 5;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.thorns += 0.333333343f;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ClothArmor"));
            recipe.AddIngredient(mod.ItemType("ClothArmor"));
            recipe.AddIngredient(ItemID.GoldCoin, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
