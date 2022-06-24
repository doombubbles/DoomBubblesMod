namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public class QuartzGauntlet : GauntletItem
{
    protected override int GemID => ItemID.Diamond;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        Tooltip.SetDefault("5% increased crit chance");
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetCritChance(DamageClass.Generic) += 5;
    }
}