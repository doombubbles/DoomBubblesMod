using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
	public class Doomhammer : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 200;
			item.melee = true;
			item.width = 44;
			item.height = 44;

			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = 640000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
            item.useTurn = false;
            item.crit = 4;
            item.scale = 1.1f;
            projOnSwing = true;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Doomhammer");
      Tooltip.SetDefault("");
    }

        /*
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TrueExcalibur, 1);
            recipe.AddIngredient(ItemID.EyeoftheGolem, 1);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        */
        

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (player.altFunctionUse != 2 && player.itemAnimation == player.itemAnimationMax - 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("DoomhammerGlow"), item.damage, 5f, player.whoAmI, player.itemAnimation);
            }

            if (player.altFunctionUse == 2)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 160);
            }
            else
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);

            }
        }
        
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                int posX = ((int)player.Center.X + ((Main.mouseX - (Main.screenWidth / 2))));
                int posY = ((int)player.Center.Y + ((Main.mouseY - (Main.screenHeight / 2))));
                Projectile.NewProjectile((float)posX + 6, (float)posY + 6, 0, 0, mod.ProjectileType("DoomLightning"), item.damage / 5, 0f, player.whoAmI);
            }
            
            else
            {
                //Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("DoomhammerGlow"), item.damage, 5f, player.whoAmI, item.useAnimation);
            }
            
            return true;
        }



    }
}
