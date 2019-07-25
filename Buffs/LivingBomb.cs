using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs
{
    public class LivingBomb : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            canBeCleared = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.onFire = true;
        }
    }
}