namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public class SaffronGauntlet : GauntletItem
{
    protected override int GemID => ItemID.Topaz;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        // Tooltip.SetDefault("5% increased thrown damage");
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(0, 1);
        Item.width = 36;
        Item.height = 40;
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Throwing) += .05f;
    }
}