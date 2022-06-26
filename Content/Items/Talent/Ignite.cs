using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class Ignite : TalentItem<FlamestrikeTome>
{
    public override string Description => "Flamestrike applies Living Bomb to 1 enemy\n" +
                                          "[Right Click on a Flamestrike Tome with this to apply]";

    protected override int RarityColor => ItemRarityID.Lime;
}