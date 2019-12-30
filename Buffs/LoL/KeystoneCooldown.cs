using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace DoomBubblesMod.Buffs.LoL
{
	public class KeystoneCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Keystone Cooldown");
			Description.SetDefault("");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
		}
		
        public override bool ReApply(Player player, int time, int buffIndex)
        {
	        player.buffTime[buffIndex] = time;
            return true;
        }

    }
}
