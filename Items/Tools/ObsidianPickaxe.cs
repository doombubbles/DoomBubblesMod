using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Tools
{
    public class ObsidianPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            item.height = 32;
            item.width = 32;
            item.damage = 8;
            item.knockBack = 2.5f;
            item.useTime = 23;
            item.useAnimation = 23;
            item.pick = 65;
            item.useStyle = 1;
            item.value = Item.sellPrice(0, 0, 40);
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Obsidian, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.needLava = true;
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}