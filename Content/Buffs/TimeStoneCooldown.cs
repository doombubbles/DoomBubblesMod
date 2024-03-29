namespace DoomBubblesMod.Content.Buffs;

public class TimeStoneCooldown : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Time Stone Cooldown");
        Description.SetDefault("");
        Main.debuff[Type] = true;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = false;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
    }

    public override bool ReApply(NPC npc, int time, int buffIndex)
    {
        return true;
    }

    public override bool ReApply(Player player, int time, int buffIndex)
    {
        return true;
    }
}