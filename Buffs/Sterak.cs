using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs
{
	public class Sterak : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Sterak's Fury");
			Description.SetDefault("25% increased melee crit and defense");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
			longerExpertDebuff = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.meleeCrit += 25;
            player.statDefense = (int)(player.statDefense * 1.25);
		}
	}
}
