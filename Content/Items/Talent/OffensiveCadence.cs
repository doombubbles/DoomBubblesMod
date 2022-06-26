using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class OffensiveCadence : TalentItem<RepeaterCannon>
{
    public override string Description => "Every third shot is empowered\n" +
                                          "[Right Click on a Repeater Cannon with this to apply]";

    protected override int RarityColor => ItemRarityID.Orange;
}