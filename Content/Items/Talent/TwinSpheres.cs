using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class TwinSpheres : TalentItem<VerdantSpheres>
{
    public override string Description => "Base Verdant Sphere bonuses are doubled\n" +
                                          "[Right Click on a Verdant Spheres with this to apply]";

    protected override int RarityColor => ItemRarityID.Lime;
}