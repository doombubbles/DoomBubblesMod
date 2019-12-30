using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Cleaved : ModBuff
	{
		public int stacks;
		
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Cleaved");
			Description.SetDefault("Reduced armor");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
	        npc.buffTime[buffIndex]++;

            if ((int) Main.time % 10 == 0)
            {
	            npc.buffTime[buffIndex] -= 10;
            }
            
            int stacks = 1 + npc.buffTime[buffIndex] % 10;

            npc.defense = (int)(npc.defense * (1f - stacks * .04f));
        }

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
	        int stacks = 1 + npc.buffTime[buffIndex] % 10;
	        if (stacks < 6) stacks++;
	        npc.buffTime[buffIndex] = 299 + stacks;
            return true;
        }

    }
}
