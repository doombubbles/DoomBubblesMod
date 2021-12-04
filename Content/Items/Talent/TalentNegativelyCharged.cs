using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Talent;

public class TalentNegativelyCharged : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Negatively Charged");
        Tooltip.SetDefault("Lightning Surge Talent\n" +
                           "Further increased center damage bonus\n" +
                           "[Right Click on a Lightning Surge with this to apply]");
        Item.SetResearchAmount(1);
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