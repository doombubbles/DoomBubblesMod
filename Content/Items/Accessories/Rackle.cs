namespace DoomBubblesMod.Content.Items.Accessories;

public class Rackle : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("'...and sometimes 5 isn't enough either'");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Shackle);
        Item.defense = 25;
    }
    
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient<Stackle>(5);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}