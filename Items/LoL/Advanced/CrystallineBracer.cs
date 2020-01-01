using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    class CrystallineBracer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased maximum life by 20\n" +
                               "Increased life regeneration");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 6, 50);
            item.width = 30;
            item.height = 30;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.lifeRegen += 1;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RubyCrystal"));
            recipe.AddIngredient(mod.ItemType("RejuvenationBead"));
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
