using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class Dissonance : TalentItem<DiscordBlade>
{
    public override string Description => "Discord Strike has signficantly longer range\n" +
                                          "[Right Click on a Discord Blade with this to apply]";

    protected override int RarityColor => ItemRarityID.Red;
}