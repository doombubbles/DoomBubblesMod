using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Electrocute : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Electrocute");
			Description.SetDefault("Hit the same target 3 times in a row");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			longerExpertDebuff = false;
		}
    }
}
