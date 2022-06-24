namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public class JadeGauntlet : GauntletItem
{
    protected override int GemID => ItemID.Emerald;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        Tooltip.SetDefault("5% increased ranged damage");
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Ranged) += .05f;
    }
}