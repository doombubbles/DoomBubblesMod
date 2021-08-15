using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Tools
{
    public class ObsidianHammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.height = 32;
            Item.width = 32;
            Item.damage = 8;
            Item.knockBack = 5f;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.hammer = 55;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 0, 40);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.AddCondition(Recipe.Condition.NearLava);
            recipe.Register();
        }
    }
}