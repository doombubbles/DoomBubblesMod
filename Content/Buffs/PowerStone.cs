using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Buffs;

public class PowerStone : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Power Stoned");
        Description.SetDefault("");
        Main.debuff[Type] = false;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = false;
        BuffID.Sets.LongerExpertDebuff[Type] = false;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ThanosPlayer>().powerStone = true;
    }

    public override bool ReApply(Player player, int time, int buffIndex)
    {
        player.buffTime[buffIndex] += time;
        if (player.buffTime[buffIndex] > 1800)
        {
            player.buffTime[buffIndex] = 1800;
        }

        return true;
    }
}