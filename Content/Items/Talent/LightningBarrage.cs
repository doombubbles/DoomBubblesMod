using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class LightningBarrage : TalentItem<LightningSurge>
{
    public override string Description => "Can zap up to 2 enemies at once\n" +
                                          "[Right Click on a Lightning Surge with this to apply]";

    protected override int RarityColor => ItemRarityID.Red;
}