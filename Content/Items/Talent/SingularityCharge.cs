using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class SingularityCharge : TalentItem<PhaseBombLauncher>
{
    public override string Description => "Explosions that only hit 1 enemy deal bonus damage\n" +
                                          "[Right Click on a Phase Bomb Launcher with this to apply]";

    protected override int RarityColor => ItemRarityID.Orange;
}