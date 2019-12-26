using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class DarkHarvest : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Dark Harvest");
			Description.SetDefault("");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			longerExpertDebuff = false;
		}

		public override void ModifyBuffTip(ref string tip, ref int rare)
		{
			int souls = Main.LocalPlayer.GetModPlayer<LoLPlayer>().darkHarvestSouls;
			tip = "Souls: " + souls;
		}
    }
}
