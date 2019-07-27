﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    class StardustPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Stardust Pouch");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("StardustBullet");
            item.width = 26;
            item.height = 34;
            item.ammo = AmmoID.Bullet;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.ranged = true;
            item.rare = 10;
            item.damage = 17;
        }
        public override void AddRecipes()
        {
            
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("StardustBullet"), 3996);
            recipe.SetResult(this);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddRecipe();
        }
    }
}
