using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public class ObsidianAxe : ModItem
    {
        public override void SetDefaults()
        {
            item.height = 28;
            item.width = 32;
            item.damage = 18;
            item.knockBack = 5f;
            item.useTime = 30;
            item.useAnimation = 30;
            item.axe = 14;
            item.useStyle = 1;
            item.value = Item.sellPrice(0, 0, 40, 0);
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Obsidian, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.needLava = true;
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}