namespace DoomBubblesMod.Content.Items.Talent;

public class TalentFuryOfTheSunwell : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Fury of the Sunwell");
        Tooltip.SetDefault("Flamestrike Talent\n" +
                           "Flamestrikes explode a second time\n" +
                           "[Right Click on a Flamestrike Tome with this to apply]");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.maxStack = 1;
        Item.value = Item.buyPrice(0, 42);
        Item.rare = ItemRarityID.Lime;
    }
}