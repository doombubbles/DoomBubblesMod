using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DoomBubblesMod.Items.LoL.Advanced
{
	public class CaulfieldsWarhammer : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Caulfield's Warhammer");
			Tooltip.SetDefault("10% increased cooldown reduction");
		}

		public override void SetDefaults() {
			item.damage = 25;
			item.melee = true;
			item.width = 36;
			item.height = 36;
			item.useTime = 15;
			item.useAnimation = 15;
			item.hammer = 75;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = Item.buyPrice(0, 11);
			item.rare = 4;
			item.scale = 1.2f;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<LoLPlayer>().cdr += .1f;
			base.UpdateAccessory(player, hideVisual);
		}

		public override void HoldItem(Player player)
		{
			player.GetModPlayer<LoLPlayer>().cdr += .1f;
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
			recipe.AddIngredient(mod.ItemType("LongSword"));
			recipe.AddIngredient(mod.ItemType("LongSword"));
			recipe.AddIngredient(ItemID.GoldCoin, 4);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		
	}
}