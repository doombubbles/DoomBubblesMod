using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class WarmogsHeart : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Warmog's Heart");
			Description.SetDefault("Super life regen");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			longerExpertDebuff = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen += (int) (player.statLifeMax * .025);
			player.dryadWard = true;
		}
	}
}
