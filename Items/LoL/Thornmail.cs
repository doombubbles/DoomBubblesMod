using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    class Thornmail : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Thorns effect and 25 increased maximum life\n" +
                               "Increased immune time from contact damage");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 29);
            item.width = 40;
            item.height = 34;
            item.rare = 8;
            item.accessory = true;
            item.defense = 16;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().coldSteel = true;
            player.thorns += 0.333333343f;
            player.statLifeMax2 += 25;
            player.thorns += 0.333333343f;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BrambleVest"));
            recipe.AddIngredient(mod.ItemType("RubyCrystal"));
            recipe.AddIngredient(mod.ItemType("WardensMail"));
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
