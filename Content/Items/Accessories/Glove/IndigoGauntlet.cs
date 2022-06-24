namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public class IndigoGauntlet : GauntletItem
{
    protected override int GemID => ItemID.Amethyst;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        Tooltip.SetDefault("5% reduced damage taken");
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.endurance += .05f;
    }
}