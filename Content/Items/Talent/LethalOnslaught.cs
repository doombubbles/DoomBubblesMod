using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class LethalOnslaught : TalentItem<DiscordBlade>
{
    public override string Description => "Discord Strike does bonus damage to low health enemies\n" +
                                          "[Right Click on a Discord Blade with this to apply]";

    protected override int RarityColor => ItemRarityID.Red;
}