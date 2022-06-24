namespace DoomBubblesMod.Content.Items.Weapons;

public class MidnightMaelstrom : NightsEdgeGun
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Midnight Maelstrom");
        SacrificeTotal = 1;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Musket);
        recipe.AddIngredient(ItemID.Boomstick);
        recipe.AddIngredient(ItemID.PhoenixBlaster);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();

        var recipe2 = CreateRecipe();
        recipe2.AddIngredient(ItemType<BloodburstBlunderbuss>());
        recipe2.AddTile(TileID.DemonAltar);
        recipe2.Register();
    }

    // Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2, 0);
    }
}