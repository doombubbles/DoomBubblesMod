using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Weapons
{
	public class Ultrashark : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("50% chance to not consume ammo\n" +
			                   "'It came from the edge of Minishark's cool uncle'");
			DisplayName.SetDefault("Ultrashark");
		}

		public override void SetDefaults()
		{
			item.damage = 65;
			item.ranged = true;
			item.width = 90;
			item.height = 32;
			item.useTime = 6;
			item.useAnimation = 6;
			item.crit = 5;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4;
			item.value = Item.sellPrice(0, 10, 50, 0);
			item.rare = 8;
			item.UseSound = SoundID.Item40;
			item.autoReuse = true;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 11f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .9f;
		}
		
		// What if I wanted this gun to have a 38% chance not to consume ammo?
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .50f;
		}

		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
		

		// Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, -3);
		}
	}
}
