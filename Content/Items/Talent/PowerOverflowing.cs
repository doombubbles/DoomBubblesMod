using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class PowerOverflowing : TalentItem<PylonStaff>
{
    public override string Description => "Your Pylons buff player damage by 15%\n" +
                                          "[Right Click on a Pylon Staff with this to apply]";

    protected override int RarityColor => ItemRarityID.Yellow;
}