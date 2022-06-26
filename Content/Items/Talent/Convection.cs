using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class Convection : TalentItem<FlamestrikeTome>
{
    public override string Description => "Flamestrike hits give a stacking buff that's lost on death\n" +
                                          "[Right Click on a Flamestrike Tome with this to apply]";

    protected override int RarityColor => ItemRarityID.Lime;
}