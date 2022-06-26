using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class SustainingPower : TalentItem<LightningSurge>
{
    public override string Description => "Heal for enemies hit other than the target\n" +
                                          "[Right Click on a Lightning Surge with this to apply]";

    protected override int RarityColor => ItemRarityID.Red;
}