namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public class SepiaGauntlet : GauntletItem
{
    protected override int GemID => ItemID.Amber;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        // Tooltip.SetDefault("5% increased summon damage");
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Summon) += .05f;
    }
}