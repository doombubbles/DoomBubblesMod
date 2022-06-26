namespace DoomBubblesMod.Content.Items.Ammo;

public class EndlessStardustPouch : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Splits into smaller bullets on hit");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.shoot = ProjectileType<Projectiles.Ranged.StardustBullet>();
        Item.width = 26;
        Item.height = 34;
        Item.ammo = AmmoID.Bullet;
        Item.value = Item.sellPrice(0, 4);
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Red;
        Item.damage = 17;
        Item.knockBack = 3;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemType<StardustBullet>(), 3996);
        recipe.ReplaceResult(this);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}