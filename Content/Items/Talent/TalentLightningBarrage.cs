namespace DoomBubblesMod.Content.Items.Talent;

public class TalentLightningBarrage : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Lightning Barrage");
        Tooltip.SetDefault("Lightning Surge Talent\n" +
                           "Can zap up to 2 enemies at once\n" +
                           "[Right Click on a Lightning Surge with this to apply]");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.maxStack = 1;
        Item.value = Item.buyPrice(0, 42);
        Item.rare = ItemRarityID.Red;
    }
}