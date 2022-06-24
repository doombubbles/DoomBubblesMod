namespace DoomBubblesMod.Content.Items.Talent;

public class TalentPyromaniac : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Pyromaniac");
        Tooltip.SetDefault("Living Bomb Talent\n" +
                           "Reapplying Living Bomb instantly triggers it\n" +
                           "[Right Click on a Living Bomb Wand with this to apply]");
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