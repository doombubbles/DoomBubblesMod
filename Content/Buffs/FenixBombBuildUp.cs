using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Buffs;

public class FenixBombBuildUp : ModBuff
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Phase Bomb Build Up");
        // Description.SetDefault("");
        Main.debuff[Type] = false;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = false;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
    }

    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        var shots = Main.LocalPlayer.GetModPlayer<HotsPlayer>().fenixBombBuildUp;
        tip = "Stacks: " + shots;
    }

    public override bool ReApply(NPC npc, int time, int buffIndex) => false;

    public override bool ReApply(Player player, int time, int buffIndex) => false;
}