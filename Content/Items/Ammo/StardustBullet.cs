namespace DoomBubblesMod.Content.Items.Ammo;

public class StardustBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        // Tooltip.SetDefault("Splits into smaller bullets on hit");
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.MoonlordBullet);
        Item.shoot = ProjectileType<Projectiles.Ranged.StardustBullet>();
        Item.shootSpeed = 1.5f;
        Item.damage = 17;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe(111);
        recipe.AddIngredient(ItemID.FragmentStardust);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}