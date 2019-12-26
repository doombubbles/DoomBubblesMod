using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class GraspOfTheUndying : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Grasp of the Undying");
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
			double stacks = Main.LocalPlayer.GetModPlayer<LoLPlayer>().grasp;
			tip = "Stacks: " + stacks;
		}

		public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return false;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return false;
        }

    }
}
