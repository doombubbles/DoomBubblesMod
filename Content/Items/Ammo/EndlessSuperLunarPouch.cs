using DoomBubblesMod.Content.Projectiles.Ranged;

namespace DoomBubblesMod.Content.Items.Ammo;

public class EndlessSuperLunarPouch : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Effects of all Lunar Bullets combined");
        Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(15, 4));
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.shoot = ProjectileType<SuperLunarBullet>();
        Item.width = 26;
        Item.height = 34;
        Item.ammo = AmmoID.Bullet;
        Item.value = Item.sellPrice(0, 20);
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Red;
        Item.damage = 20;
        Item.knockBack = 3;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemType<EndlessSolarPouch>());
        recipe.AddIngredient(ItemType<EndlessNebulaPouch>());
        recipe.AddIngredient(ItemType<EndlessVortexPouch>());
        recipe.AddIngredient(ItemType<EndlessStardustPouch>());
        recipe.AddIngredient(ItemID.GravityGlobe);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}