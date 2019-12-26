using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Aery : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Summoned Aery");
			Description.SetDefault("I'm not good at NPC sprites, ok?");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			longerExpertDebuff = false;
		}

    }
}
