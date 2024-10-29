namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public class CrimsonGauntlet : GauntletItem
{
    protected override int GemID => ItemID.Ruby;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        // Tooltip.SetDefault("5% increased melee damage");
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Melee) += .05f;
    }
}