using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoomBubblesMod.Items
{
	public class TrueTrigun : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("25% chance not to consume ammo\n."
								+ "Shoots in three round bursts with True Bullets\n");
			DisplayName.SetDefault("True Trigun");
		}

		public override void SetDefaults()
		{
			item.damage = 45;
			item.ranged = true;
			item.width = 56;
			item.height = 26;
			item.useTime = 3;
			item.useAnimation = 9;
			item.reuseDelay = 14;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4;
			item.value = Item.sellPrice(0, 10);
			item.rare = 8;
			item.UseSound = SoundID.Item11;
			item.autoReuse = false;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			if (DoomBubblesMod.thoriumLoaded)
			{
				addThoriumRecipe();
			}
			else
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.HallowedBar, 9);
				recipe.AddIngredient(mod.ItemType("BrokenHeroGun"));
				recipe.AddTile(TileID.MythrilAnvil);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}

		private void addThoriumRecipe()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("Trigun"));
			recipe.AddIngredient(mod.ItemType("BrokenHeroGun"));
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool ConsumeAmmo(Player player)
		{
			if (Main.rand.NextFloat() <= 25f)
			{
				return false;
			}
			if (!(player.itemAnimation < item.useAnimation - 2))
			{
				return false;
			}

			return base.ConsumeAmmo(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			
			int to8 = Math.Abs(player.itemAnimation - 8);
			int to5 = Math.Abs(player.itemAnimation - 5);
			int to2 = Math.Abs(player.itemAnimation - 2);
			int closest = Math.Min(Math.Min(to2, to5), to8);
			
			if (closest == to8)
			{
				type = mod.ProjectileType("TruePiercingBullet");
			}
			else if (closest == to2)
			{
				type = mod.ProjectileType("TrueHomingBullet");
			}
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);
		}

	}
}
