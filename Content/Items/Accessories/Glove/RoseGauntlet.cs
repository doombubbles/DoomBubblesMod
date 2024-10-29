using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public class RoseGauntlet : GauntletItem
{
    protected override string GemName => "Opal";

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        // Tooltip.SetDefault("5% increased symphonic damage");
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.SymphonicDamage(f => f + .05f);
    }
}