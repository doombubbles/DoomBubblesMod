namespace DoomBubblesMod.Content.Items.Talent;

public class TalentDissonance : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Dissonance");
        Tooltip.SetDefault("Discord Strike Talent\n" +
                           "Discord Strike has signficantly longer range\n" +
                           "[Right Click on a Discord Blade with this to apply]");
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