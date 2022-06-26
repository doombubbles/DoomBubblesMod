namespace DoomBubblesMod.Content.Items.Weapons;

public class TrueMidnightMaelstrom : TrueNightsEdgeGun
{
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemType<MidnightMaelstrom>());
        recipe.AddIngredient(ItemID.SoulofFright, 5);
        recipe.AddIngredient(ItemID.SoulofMight, 5);
        recipe.AddIngredient(ItemID.SoulofSight, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2, 0);
    }
}