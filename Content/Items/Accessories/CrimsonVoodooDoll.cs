namespace DoomBubblesMod.Content.Items.Accessories;

public class CrimsonVoodooDoll : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Your life regenerates crimsonly");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GuideVoodooDoll);
    }
    
    public override bool IsLoadingEnabled(Mod mod) => SetBonusAccessories == null;
    
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.crimsonRegen = true;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GuideVoodooDoll);
        recipe.AddIngredient(ItemID.TissueSample, 5);
        recipe.AddIngredient(ItemID.CrimsonHelmet);
        recipe.AddIngredient(ItemID.CrimsonScalemail);
        recipe.AddIngredient(ItemID.CrimsonGreaves);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
    }
}