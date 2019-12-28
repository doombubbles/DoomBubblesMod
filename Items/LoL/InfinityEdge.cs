using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
	public class InfinityEdge : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 200;
			item.melee = true;
			item.width = 48;
			item.height = 48;

			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = 640000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.useTurn = false;
            item.crit = 20;
            item.scale = 1.1f;
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Edge");
            Tooltip.SetDefault("Increased critical strike power");
        }


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TrueExcalibur, 1);
            recipe.AddIngredient(mod.ItemType("CloakOfAgility"), 1);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 57);
			}
            
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (crit)
            {
                damage *= 2;
            }
        }

        public override void ModifyHitPvp(Player player, Player target, ref int damage, ref bool crit)
        {
            if (crit)
            {
                damage *= 2;
            }
        }

    }
}
