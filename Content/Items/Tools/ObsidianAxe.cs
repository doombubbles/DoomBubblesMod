using Terraria;
namespace DoomBubblesMod.Content.Items.Tools;

public class ObsidianAxe : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.height = 28;
        Item.width = 32;
        Item.damage = 18;
        Item.knockBack = 5f;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.axe = 14;
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
        recipe.AddCondition(Condition.NearLava);
        recipe.Register();
    }
}