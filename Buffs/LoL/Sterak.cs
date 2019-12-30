using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Sterak : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Sterak's Fury");
			Description.SetDefault("Increased defense based on bonus health (decaying)");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
			longerExpertDebuff = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.statDefense += (int) ((player.statLifeMax2 - player.statLifeMax) * (player.buffTime[buffIndex] / 480f));
		}

		public override bool ReApply(Player player, int time, int buffIndex)
		{
			return true;
		}
	}
}
