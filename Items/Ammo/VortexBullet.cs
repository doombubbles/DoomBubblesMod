using DoomBubblesMod.Utils;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Ammo
{
    public class VortexBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vortex Bullet");
            Tooltip.SetDefault("Creates bullet echos on enemy hits");
            Item.SetResearchAmount(99);
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MoonlordBullet);
            Item.shoot = ModContent.ProjectileType<Projectiles.VortexBullet>();
            Item.shootSpeed = 1.5f;
            Item.damage = 17;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentVortex);
            recipe.ReplaceResult(this, 111);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}