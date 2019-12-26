using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
    public class LethalTempoWindUp : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lethal Tempo Wind Up");
            Description.SetDefault("");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] == 1)
            {
                player.DelBuff(buffIndex);
                player.AddBuff(mod.BuffType("LethalTempo"), 6 * 60);
            }

            if (player.buffTime[buffIndex] < 60)
            {
                Dust.NewDustPerfect(player.Center + new Vector2(-20, 20), 57, player.velocity / 2 + new Vector2(-.2f, -1));
            }
            Dust.NewDustPerfect(player.Center + new Vector2(20, 20), 57, player.velocity / 2 + new Vector2(.2f, -1));
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return true;
        }
    }
}