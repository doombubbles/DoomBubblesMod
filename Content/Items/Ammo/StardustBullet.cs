using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Ammo;

public class StardustBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Stardust Bullet");
        Tooltip.SetDefault("Splits into smaller bullets on hit");
        Item.SetResearchAmount(99);
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.MoonlordBullet);
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.StardustBullet>();
        Item.shootSpeed = 1.5f;
        Item.damage = 17;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FragmentStardust);
        recipe.ReplaceResult(this, 111);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}