using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace DoomBubblesMod.Buffs
{
	public class FenixRepeaterBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Repeater Buff");
			Description.SetDefault("");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
		}

        public override void Update(NPC npc, ref int buffIndex)
		{
            
        }
		
		public override void ModifyBuffTip(ref string tip, ref int rare)
		{
			int shots = Main.LocalPlayer.GetModPlayer<HotSPlayer>().fenixRepeaterBuff;
			tip = "Stacks: " + shots;
		}

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return !player.gravControl2;
        }

    }
}
