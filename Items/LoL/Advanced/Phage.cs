using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DoomBubblesMod.Items.LoL.Advanced
{
	public class Phage : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Phage");
			Tooltip.SetDefault("Enrages you\n" +
			                   "Equipped - 20 bonus life");
		}

		public override void SetDefaults() {
			item.damage = 35;
			item.melee = true;
			item.width = 36;
			item.height = 36;
			item.useTime = 20;
			item.useAnimation = 20;
			item.hammer = 75;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = Item.buyPrice(0, 12, 50);
			item.rare = 4;
			item.scale = 1.2f;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<LoLPlayer>().rage = true;
			player.statLifeMax2 += 20;
			base.UpdateAccessory(player, hideVisual);
		}

		public override void HoldItem(Player player)
		{
			player.GetModPlayer<LoLPlayer>().rage = true;
			base.HoldItem(player);
		}

		public override void UpdateInventory(Player player)
		{
			item.accessory = true;
			base.UpdateInventory(player);
		}
		
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("RubyCrystal"));
			recipe.AddIngredient(mod.ItemType("LongSword"));
			recipe.AddIngredient(ItemID.GoldCoin, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		
	}
}