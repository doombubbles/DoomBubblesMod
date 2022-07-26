namespace DoomBubblesMod.Content.Items.Ammo;

public class VortexBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Creates bullet echos on enemy hits");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.MoonlordBullet);
        Item.shoot = ProjectileType<Projectiles.Ranged.VortexBullet>();
        Item.shootSpeed = 1.5f;
        Item.damage = 17;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe(111);
        recipe.AddIngredient(ItemID.FragmentVortex);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}