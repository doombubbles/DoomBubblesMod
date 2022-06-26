using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class NegativelyCharged : TalentItem<LightningSurge>
{
    public override string Description => "Further increased center damage bonus\n" +
                                          "[Right Click on a Lightning Surge with this to apply]";

    protected override int RarityColor => ItemRarityID.Red;
}