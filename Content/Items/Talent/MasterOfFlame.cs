using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class MasterOfFlame : TalentItem<LivingBombWand>
{
    public override string Description => "Living Bomb can spread indefinitely\n" +
                                          "[Right Click on a Living Bomb Wand with this to apply]";

    protected override int RarityColor => ItemRarityID.Lime;
}