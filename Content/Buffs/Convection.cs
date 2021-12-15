using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Buffs;

public class Convection : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Convection");
        Description.SetDefault("Flamestrike Damage Bonus: 0");
        Main.debuff[Type] = false;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = false;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.buffTime[buffIndex] = 10;
    }

    public override void ModifyBuffTip(ref string tip, ref int rare)
    {
        var shots = Main.LocalPlayer.GetModPlayer<HotSPlayer>().convection;
        tip = "Flamestrike Damage Bonus: " + shots;
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