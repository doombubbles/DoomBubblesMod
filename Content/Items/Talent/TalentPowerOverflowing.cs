namespace DoomBubblesMod.Content.Items.Talent;

public class TalentPowerOverflowing : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Power Overflowing");
        Tooltip.SetDefault("Pylon Talent\n" +
                           "Your Pylons buff player damage by 15%\n" +
                           "[Right Click on a Pylon Staff with this to apply]");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.maxStack = 1;
        Item.value = Item.buyPrice(0, 42);
        Item.rare = ItemRarityID.Yellow;
    }
}