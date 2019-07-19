using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class Botrk : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade of the Ruined King");
            Tooltip.SetDefault("Stronger against more powerful enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 100;
            item.melee = true;
            item.width = 54;
            item.height = 54;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = 640000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Potrk");
            item.shootSpeed = 10f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TrueNightsEdge, 1);
            recipe.AddIngredient(ItemID.GoldCrown, 1);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.TrueNightsEdge, 1);
            recipe2.AddIngredient(ItemID.PlatinumCrown, 1);
            recipe2.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe2.AddTile(TileID.DemonAltar);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
        

    }
}
