using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class ConstructAdditionalPylons : TalentItem<PylonStaff>
{
    public override string Description => "Up to 3 Pylons with bigger range\n" +
                                          "[Right Click on a Pylon Staff with this to apply]";

    protected override int RarityColor => ItemRarityID.Yellow;
}