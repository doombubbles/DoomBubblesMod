namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public class EbonyGauntlet : GauntletItem
{
    protected override string GemName => "Onyx";

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        // Tooltip.SetDefault("Increases armor penetration by 5");
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetArmorPenetration(DamageClass.Generic) += 5;
    }
}