using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs
{
    public class LivingBomb : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            CanBeCleared = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.onFire = true;
        }
    }
}