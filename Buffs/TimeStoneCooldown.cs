using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace DoomBubblesMod.Buffs
{
	public class TimeStoneCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Time Stone Cooldown");
			Description.SetDefault("");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
		}

        public override void Update(NPC npc, ref int buffIndex)
		{
            
        }

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return true;
        }

    }
}
