namespace DoomBubblesMod.Content.Items.Ammo;

public class TheObservableUniverse : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pocket Galaxy");
        Tooltip.SetDefault("An endless amount of stars");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.shoot = ProjectileID.FallingStar;
        Item.width = 38;
        Item.height = 38;
        Item.ammo = AmmoID.FallenStar;
        Item.value = Item.sellPrice(0, 2);
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Orange;
    }


    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FallenStar, 999);
        recipe.ReplaceResult(this);
        recipe.AddTile(TileID.CrystalBall);
        recipe.Register();
    }
}