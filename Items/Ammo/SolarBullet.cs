using DoomBubblesMod.Utils;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Ammo
{
    public class SolarBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Bullet");
            Tooltip.SetDefault("Deals bonus damage to airborne enemies");
            Item.SetResearchAmount(99);
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MoonlordBullet);
            Item.shoot = ModContent.ProjectileType<Projectiles.SolarBullet>();
            Item.shootSpeed = 1.5f;
            Item.damage = 17;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentSolar);
            recipe.ReplaceResult(this, 111);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}