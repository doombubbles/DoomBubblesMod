using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class Pyromaniac : TalentItem<LivingBombWand>
{
    public override string Description => "Reapplying Living Bomb instantly triggers it\n" +
                                          "[Right Click on a Living Bomb Wand with this to apply]";

    protected override int RarityColor => ItemRarityID.Lime;
}