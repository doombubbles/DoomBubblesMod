using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    class SuperLunarPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Super Lunar Pouch");
            Tooltip.SetDefault("");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(15, 4));
        }

        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("SuperLunarBullet");
            item.width = 26;
            item.height = 34;
            item.ammo = AmmoID.Bullet;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.ranged = true;
            item.rare = 10;
            item.damage = 20;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SolarPouch"));
            recipe.AddIngredient(mod.ItemType("NebulaPouch"));
            recipe.AddIngredient(mod.ItemType("VortexPouch"));
            recipe.AddIngredient(mod.ItemType("StardustPouch"));
            recipe.AddIngredient(ItemID.GravityGlobe);
            recipe.SetResult(this);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddRecipe();
        }
    }
}
