namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public class AzureGauntlet : GauntletItem
{
    protected override int GemID => ItemID.Sapphire;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        Tooltip.SetDefault("5% increased magic damage");
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Magic) += .05f;
    }
}