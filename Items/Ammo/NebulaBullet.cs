using DoomBubblesMod.Utils;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Ammo
{
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
            Item.shoot = ModContent.ProjectileType<Projectiles.NebulaBullet>();
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
}