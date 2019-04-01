using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    class CrystalCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Core");
            Tooltip.SetDefault("Crystal Bullets release extra shards");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 2, 0 ,0);
            item.width = 26;
            item.height = 26;
            item.rare = 4;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>().crystalBulletBonus = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrystalShard, 100);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
