using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    class RecurveBow : ModItem
    {
        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 10);
            item.rare = 4;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.width = 18;
            item.height = 56;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.damage = 40;
            item.shootSpeed = 10.0f;
            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item5;
            item.knockBack = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Dagger"));
            recipe.AddIngredient(mod.ItemType("Dagger"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(5, 0);
        }
        
    }
}