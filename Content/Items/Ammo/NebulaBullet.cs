namespace DoomBubblesMod.Content.Items.Ammo;

public class NebulaBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Teleports to enemies if close");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.MoonlordBullet);
        Item.shoot = ProjectileType<Projectiles.Ranged.NebulaBullet>();
        Item.shootSpeed = 1.5f;
        Item.damage = 17;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe(111);
        recipe.AddIngredient(ItemID.FragmentNebula);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}