using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Ammo;

public class NebulaBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Nebula Bullet");
        Tooltip.SetDefault("Teleports to enemies if close");
        Item.SetResearchAmount(99);
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
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FragmentNebula);
        recipe.ReplaceResult(this, 111);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}