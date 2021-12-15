using DoomBubblesMod.Content.Projectiles.Ranged;
using DoomBubblesMod.Utils;
using Terraria.DataStructures;

namespace DoomBubblesMod.Content.Items.Ammo;

internal class SuperLunarPouch : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Endless Super Lunar Pouch");
        Tooltip.SetDefault("Effects of all Lunar Bullets combined");
        Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(15, 4));
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.shoot = ModContent.ProjectileType<SuperLunarBullet>();
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
        recipe.AddIngredient(ModContent.ItemType<SolarPouch>());
        recipe.AddIngredient(ModContent.ItemType<NebulaPouch>());
        recipe.AddIngredient(ModContent.ItemType<VortexPouch>());
        recipe.AddIngredient(ModContent.ItemType<StardustPouch>());
        recipe.AddIngredient(ItemID.GravityGlobe);
        recipe.ReplaceResult(this);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}