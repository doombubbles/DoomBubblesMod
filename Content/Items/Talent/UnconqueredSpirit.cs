using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class UnconqueredSpirit : TalentItem<ShieldCapacitor>
{
    public override string Description => "Upon taking fatal damage, prevent it and refresh Shield Capacitor. 2 min cooldown.\n" +
                                          "[Right Click on a Shield Capacitor with this to apply]";

    protected override int RarityColor => ItemRarityID.Orange;
}