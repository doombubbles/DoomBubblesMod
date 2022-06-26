using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class MobileOffense : TalentItem<RepeaterCannon>
{
    public override string Description => "Damage increases with player velocity\n" +
                                          "[Right Click on a Repeater Cannon with this to apply]";

    protected override int RarityColor => ItemRarityID.Orange;
}