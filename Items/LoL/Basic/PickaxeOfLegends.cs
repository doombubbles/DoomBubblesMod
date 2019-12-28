using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DoomBubblesMod.Items.LoL.Basic
{
	public class PickaxeOfLegends : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Pickaxe of Legends");
			Tooltip.SetDefault("Legends told of pickaxe that was somehow sharp enough\nthat it could be used for its attack damage.");
		}

		public override void SetDefaults() {
			item.damage = 25;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 15;
			item.useAnimation = 15;
			item.pick = 60;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = Item.sellPrice(0, 8, 75);
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
	}
}