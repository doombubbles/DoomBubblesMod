using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class SecondaryFire : TalentItem<PhaseBombLauncher>
{
    public override string Description => "Bombs also hit enemies they pass through\n" +
                                          "[Right Click on a Phase Bomb Launcher with this to apply]";

    protected override int RarityColor => ItemRarityID.Orange;
}