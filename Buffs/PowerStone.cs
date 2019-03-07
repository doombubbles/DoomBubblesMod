using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs
{
	public class PowerStone : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Power Stoned");
			Description.SetDefault("");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
			longerExpertDebuff = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<DoomBubblesPlayer>().powerStone = true;
		}

		public override bool ReApply(Player player, int time, int buffIndex)
		{
			player.buffTime[buffIndex] += time;
			if (player.buffTime[buffIndex] > 1800)
			{
				player.buffTime[buffIndex] = 1800;
			}
			return true;
		}
	}
}
