namespace DoomBubblesMod.Content.Items.Ammo;

// TODO makes solar explosions on hit
public class SolarBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Causes solar explosions on hit");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.MoonlordBullet);
        Item.shoot = ProjectileType<Projectiles.Ranged.SolarBullet>();
        Item.shootSpeed = 1.5f;
        Item.damage = 17;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe(111);
        recipe.AddIngredient(ItemID.FragmentSolar);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}