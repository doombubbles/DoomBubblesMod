using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
	public class TerraRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("50% chance to not consume ammo\n" +
			                   "Turns Bullets into Terra Bullets");
			DisplayName.SetDefault("Terra Rifle");
		}

		public override void SetDefaults()
		{
			item.damage = 64;
			item.ranged = true;
			item.width = 64;
			item.height = 22;
			item.useTime = 9;
			item.useAnimation = 9;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 6;
			item.value = Item.sellPrice(0, 20, 0, 0);
			item.rare = 8;
			item.UseSound = SoundID.Item36;
			item.autoReuse = true;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 11f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("TrueMidnightMaelstrom"));
			recipe.AddIngredient(mod.ItemType("TrueTrigun"));
			recipe.AddIngredient(mod.ItemType("HeartOfTerraria"));
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		// What if I wanted this gun to have a 38% chance not to consume ammo?
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .50f;
		}

		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = mod.ProjectileType("TerraBullet");
			
			
			
			
			
			if (Main.rand.NextFloat() <= .2)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("MidnightBlast"), (int) (damage * 2), knockBack, player.whoAmI);
				Main.PlaySound(SoundID.Item38, position);
			}
			/* Other implementation
			else
			{
				int numberProjectiles = 2;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
					// If you want to randomize the speed to stagger the projectiles
					float scale = 1f - (Main.rand.NextFloat() * .1f);
					perturbedSpeed = perturbedSpeed * scale; 
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("TerraBullet"), (int)(.5 * damage), knockBack, player.whoAmI);
				}
			}
			*/
			
			return true;
		}
		

		// Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);
		}
	}
}
