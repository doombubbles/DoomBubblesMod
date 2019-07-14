using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace DoomBubblesMod.Items.Weapons
{
    public class RainbowMachineGun : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 105;
            item.magic = true;
            item.mana = 5;
            item.width = 20;
            item.height = 12;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2f;
            item.value = Item.sellPrice(0, 15, 0, 0);
            item.rare = 8;
            item.autoReuse = true;
            item.shootSpeed = 20f;
            item.shoot = mod.ProjectileType("RainbowMachineGun");
            item.channel = true;
            item.noUseGraphic = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rainbow Machinegun");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LaserMachinegun, 1);
            recipe.AddIngredient(ItemID.RainbowGun, 1);
            recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}