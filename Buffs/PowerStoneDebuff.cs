using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs
{
    public class PowerStoneDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Compassion");
            Description.SetDefault("At least that's what he calls it");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            LongerExpertDebuff = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DoomBubblesGlobalNPC>().powerStoned = true;

            base.Update(npc, ref buffIndex);
        }
    }
}