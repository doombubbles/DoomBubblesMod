using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class DampeningField : TalentItem<ShieldCapacitor>
{
    public override string Description => "You take 25% less damage while Shield Capacitor is above half charge\n" +
                                          "[Right Click on a Shield Capacitor with this to apply]";

    protected override int RarityColor => ItemRarityID.Orange;
}