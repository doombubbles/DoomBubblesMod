using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Buffs;

public class FenixBombBuildUp : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Phase Bomb Build Up");
        Description.SetDefault("");
        Main.debuff[Type] = false;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = false;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
    }

    public override void ModifyBuffTip(ref string tip, ref int rare)
    {
        var shots = Main.LocalPlayer.GetModPlayer<HotSPlayer>().fenixBombBuildUp;
        tip = "Stacks: " + shots;
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