﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    class SolarPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Solar Pouch");
            Tooltip.SetDefault("Deals bonus damage to airborne enemies");
        }

        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("SolarBullet");
            item.width = 26;
            item.height = 34;
            item.ammo = AmmoID.Bullet;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.ranged = true;
            item.rare = 10;
            item.damage = 17;
            item.knockBack = 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SolarBullet"), 3996);
            recipe.SetResult(this);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddRecipe();
        }
    }
}
