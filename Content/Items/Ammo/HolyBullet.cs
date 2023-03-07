using ElementalDamage.Common.Types;
using ElementalDamage.Content.DamageClasses;

namespace DoomBubblesMod.Content.Items.Ammo;

public class HolyBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Summons falling stars on impact");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.CrystalBullet);
        Item.shoot = ProjectileType<Projectiles.Ranged.HolyBullet>();
        Item.DamageType = ElementalDamageClass.Get<RangedDamageClass, Holy>();
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe(200);
        recipe.AddIngredient(ItemID.MusketBall, 200);
        recipe.AddIngredient(ItemID.PixieDust, 3);
        recipe.AddIngredient(ItemID.UnicornHorn);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}