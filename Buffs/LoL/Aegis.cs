using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Aegis : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Aegis of the Legion");
			Description.SetDefault("Increased resistances");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			longerExpertDebuff = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 3;
			player.endurance += .03f;
		}
	}
}
