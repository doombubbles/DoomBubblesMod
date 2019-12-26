using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Energized : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Energized");
			Description.SetDefault("Your next attack has bonus effects");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			longerExpertDebuff = false;
		}

    }
}
