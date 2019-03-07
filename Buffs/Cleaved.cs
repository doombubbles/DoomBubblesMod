using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace DoomBubblesMod.Buffs
{
	public class Cleaved : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Cleaved");
			Description.SetDefault(" reduced armor");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
		}

        public override void Update(NPC npc, ref int buffIndex)
		{
            DisplayName.SetDefault(npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).Cleaved * 5 + "% reduced armor");

            npc.defense *= (1 - (int)(npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).Cleaved * .05));

            if (npc.buffTime[buffIndex] == 0)
            {
                npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).Cleaved = 0;
            }
            
        }

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.buffTime[buffIndex] = 300;
            return true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] = 300;
            return true;
        }

    }
}
