using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    class WardensMail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warden's Mail");
            Tooltip.SetDefault("Increased immune time from contact damage");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 10);
            item.width = 32;
            item.height = 34;
            item.rare = 4;
            item.accessory = true;
            item.defense = 6;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().coldSteel = true;
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
