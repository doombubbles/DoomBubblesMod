namespace DoomBubblesMod.Content.Buffs;

public class WormholeSickness : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = false;
    }
}