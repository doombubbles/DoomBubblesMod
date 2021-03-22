using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Ammo
{
    internal class NebulaPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Nebula Pouch");
            Tooltip.SetDefault("Teleports to enemies if close");
        }

        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("NebulaBullet");
            item.width = 26;
            item.height = 34;
            item.ammo = AmmoID.Bullet;
            item.value = Item.sellPrice(0, 4);
            item.ranged = true;
            item.rare = 10;
            item.damage = 17;
            item.knockBack = 3;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("NebulaBullet"), 3996);
            recipe.SetResult(this);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddRecipe();
        }
    }
}