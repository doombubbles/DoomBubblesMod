using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
	public class BlackCleaver : ModItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Black Cleaver");
            Tooltip.SetDefault("Shoots an armor piercing axe");
        }

        public override void SetDefaults()
		{
			item.damage = 125;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = 640000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.useTurn = false;
            item.shoot = mod.ProjectileType("Cleaver");
            item.shootSpeed = 9f;
        }


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TheBreaker, 1);
            recipe.AddIngredient(ItemID.SharkToothNecklace, 1);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.BloodLustCluster, 1);
            recipe2.AddIngredient(ItemID.SharkToothNecklace, 1);
            recipe2.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }

    }
}
