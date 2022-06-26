namespace DoomBubblesMod.Content.Items.Accessories;

public class CardiopulmonaryMagnet : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Increases pickup range for life hearts");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.CelestialMagnet);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.lifeMagnet = true;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CelestialMagnet);
        recipe.AddIngredient(ItemID.HeartreachPotion, 5);
        recipe.AddIngredient(ItemID.LifeCrystal);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}