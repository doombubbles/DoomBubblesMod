namespace DoomBubblesMod.Content.Items.Tools;

public class ObsidianPickaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.height = 32;
        Item.width = 32;
        Item.damage = 8;
        Item.knockBack = 2.5f;
        Item.useTime = 23;
        Item.useAnimation = 23;
        Item.pick = 65;
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