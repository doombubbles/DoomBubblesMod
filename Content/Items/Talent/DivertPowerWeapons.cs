using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class DivertPowerWeapons : TalentItem<PhaseBombLauncher>
{
    public override string Description => "Slower use time; Higher damage\n" +
                                          "[Right Click on a Phase Bomb Launcher with this to apply]";

    protected override int RarityColor => ItemRarityID.Orange;
}