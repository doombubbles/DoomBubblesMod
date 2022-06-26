using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class FelInfusion : TalentItem<VerdantSpheres>
{
    public override string Description => "10% reduced damage; 25% increased magic damage\n" +
                                          "[Right Click on a Verdant Spheres with this to apply]";

    protected override int RarityColor => ItemRarityID.Lime;
}