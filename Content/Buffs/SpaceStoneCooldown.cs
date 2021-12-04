namespace DoomBubblesMod.Content.Buffs;

public class SpaceStoneCooldown : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Space Stone Cooldown");
        Description.SetDefault("");
        Main.debuff[Type] = true;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
        LongerExpertDebuff = false;
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