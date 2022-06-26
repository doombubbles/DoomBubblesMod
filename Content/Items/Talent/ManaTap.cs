using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class ManaTap : TalentItem<VerdantSpheres>
{
    public override string Description => "Magic Weapons' base mana cost is added to their damage\n" +
                                          "[Right Click on a Verdant Spheres with this to apply]";

    protected override int RarityColor => ItemRarityID.Lime;
}