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
			item.damage = 80;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 1;
			item.knockBack = 8;
			item.value = Item.buyPrice(0, 34);
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.useTurn = true;
            item.crit = 21;
            item.scale = 1.2f;
            item.shoot = mod.ProjectileType("Edge");
            item.shootSpeed = 13f;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<LoLPlayer>().iedge = true;
			player.meleeCrit += 10;
			player.magicCrit += 10;
			player.rangedCrit += 10;
			player.thrownCrit += 10;
			base.UpdateAccessory(player, hideVisual);
		}

		public override void UpdateInventory(Player player)
		{
			item.accessory = true;
			base.UpdateInventory(player);
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Edge");
            Tooltip.SetDefault("Increased critical strike power\n" +
                               "Equipped - 10% increased critical strike chance");
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("BFSword"));
			recipe.AddIngredient(mod.ItemType("PickaxeOfLegends"));
            recipe.AddIngredient(mod.ItemType("CloakOfAgility"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddIngredient(ItemID.SilverCoin, 25);
            recipe.AddTile(TileID.Autohammer);
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

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
			ref float knockBack)
		{
			Vector2 delta = (Main.MouseWorld - position).SafeNormalize(new Vector2(speedX, speedY).SafeNormalize(new Vector2(0, 0.1f)));

			Vector2 i = Main.MouseWorld;
			while (Main.screenPosition.X < i.X && Main.screenPosition.Y < i.Y &&
			       Main.screenPosition.X + Main.screenWidth > i.X && Main.screenPosition.Y + Main.screenHeight > i.Y)
			{
				i += delta;
			}

			i += delta * 10;

			Vector2 actualSpawn = i;
			
			Vector2 velocity =
				(Main.MouseWorld - actualSpawn).SafeNormalize(
					(Main.MouseWorld - position).SafeNormalize(Vector2.Zero)) * new Vector2(speedX, speedY).Length();

			Projectile.NewProjectile(actualSpawn, velocity, type, damage, knockBack, player.whoAmI, (Main.MouseWorld - actualSpawn).Length() / velocity.Length());
			return false;
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
