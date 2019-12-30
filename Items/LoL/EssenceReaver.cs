using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
	public class EssenceReaver : ModItem
	{
		
		public override void SetDefaults()
		{
			item.damage = 70;
			item.melee = true;
			item.width = 48;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 8;
			item.value = Item.buyPrice(0, 33);
			item.rare = 8;
			item.UseSound = SoundID.Item71;
			item.autoReuse = true;
            item.useTurn = true;
            item.crit = 21;
            item.scale = 1.1f;
            item.shoot = mod.ProjectileType("Reaver");
            item.shootSpeed = 10f;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<LoLPlayer>().reaver = true;
			player.GetModPlayer<LoLPlayer>().cdr += .2f;
			base.UpdateAccessory(player, hideVisual);
		}

		public override void UpdateInventory(Player player)
		{
			item.accessory = true;
			base.UpdateInventory(player);
		}

		public override void HoldItem(Player player)
		{
			player.GetModPlayer<LoLPlayer>().reaver = true;
			player.GetModPlayer<LoLPlayer>().cdr += .2f;
			base.HoldItem(player);
		}

		public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Nonmagic attacks restore up to 1.5% of missing mana\n" +
                               "20% increased cooldown reduction");
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("BFSword"));
			recipe.AddIngredient(mod.ItemType("CaulfieldsWarhammer"));
            recipe.AddIngredient(mod.ItemType("CloakOfAgility"));
            recipe.AddIngredient(ItemID.GoldCoin);
            recipe.AddTile(TileID.Autohammer);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 135);
			}
        }

    }
}
