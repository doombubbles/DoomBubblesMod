using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class PylonOvercharge : TalentItem<PylonStaff>
{
    public override string Description => "Your Pylons attack shit now\n" +
                                          "[Right Click on a Pylon Staff with this to apply]";

    protected override int RarityColor => ItemRarityID.Yellow;
}