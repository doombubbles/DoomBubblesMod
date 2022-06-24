using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public class AquamarineGauntlet : GauntletItem
{
    protected override string GemName => "Pearl";

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        Tooltip.SetDefault("5% increased radiant damage");
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.RadiantDamage(f => f + .05f);
    }
}