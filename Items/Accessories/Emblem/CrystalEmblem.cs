using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Emblem
{
    internal class CrystalEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Emblem");
            Tooltip.SetDefault("15% increased ranged damage\n" +
                               "Crystal Bullets release extra shards");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 5);
            item.width = 28;
            item.height = 28;
            item.rare = ItemRarityID.Pink;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += .15f;
            player.GetModPlayer<DoomBubblesPlayer>().crystalBulletBonus = true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AvengerEmblem);
            recipe.AddIngredient(mod.ItemType("CrystalCore"));
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}