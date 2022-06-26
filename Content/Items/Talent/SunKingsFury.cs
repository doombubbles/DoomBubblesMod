using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class SunKingsFury : TalentItem<LivingBombWand>
{
    public override string Description => "Living Bombs deal more damage after spreading\n" +
                                          "[Right Click on a Living Bomb Wand with this to apply]";

    protected override int RarityColor => ItemRarityID.Lime;
}