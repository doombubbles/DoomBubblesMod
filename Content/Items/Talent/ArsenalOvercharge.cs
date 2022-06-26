using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class ArsenalOvercharge : TalentItem<RepeaterCannon>
{
    public override string Description => "Buff stacks up to 15 shots\n" +
                                          "[Right Click on a Repeater Cannon with this to apply]";

    protected override int RarityColor => ItemRarityID.Orange;
}