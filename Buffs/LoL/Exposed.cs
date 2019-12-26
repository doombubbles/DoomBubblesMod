using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
    public class Exposed : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Exposed");
            Description.SetDefault("Taking extra damage");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }


        public override void Update(NPC npc, ref int buffIndex)
        {
            Vector2 dir = new Vector2(3, (float) Math.Sqrt(3));
            Dust.NewDustPerfect(npc.Center + dir * 3, 57, npc.velocity / 2 + dir / 2);

            dir = new Vector2(-3, (float) Math.Sqrt(3));
            Dust.NewDustPerfect(npc.Center + dir * 3, 57, npc.velocity / 2 + dir / 2);
            
            dir = new Vector2(0, (float) (-2 * Math.Sqrt(3)));
            Dust.NewDustPerfect(npc.Center + dir * 3, 57, npc.velocity / 2 + dir / 2);
        }
    }
}