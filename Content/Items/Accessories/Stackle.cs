namespace DoomBubblesMod.Content.Items.Accessories;

public class Stackle : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("'Sometimes 1 just isn't enough'");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Shackle);
        Item.defense = 5;
    }
    
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Shackle, 5);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}