using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Guardian : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Guardian");
			Description.SetDefault("Bonus Defense");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			longerExpertDebuff = false;
		}

    }
}
