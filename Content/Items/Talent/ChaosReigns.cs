using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class ChaosReigns : TalentItem<DiscordBlade>
{
    public override string Description => "Discord Strike does increasing damage as it passes through enemies\n" +
                                          "[Right Click on a Discord Blade with this to apply]";

    protected override int RarityColor => ItemRarityID.Red;
}