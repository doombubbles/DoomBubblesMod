namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public class MeteorGauntlet : GauntletItem
{
    protected override int GemID => ItemID.MeteoriteBar;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        // Tooltip.SetDefault("5% increased attack speed");
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetAttackSpeed(DamageClass.Generic) += .05f;
    }
}