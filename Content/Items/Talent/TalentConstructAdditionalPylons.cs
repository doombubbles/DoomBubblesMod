using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Talent;

public class TalentConstructAdditionalPylons : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Construct Additional Pylons");
        Tooltip.SetDefault("Pylon Talent\n" +
                           "Up to 3 Pylons with bigger range\n" +
                           "[Right Click on a Pylon Staff with this to apply]");
        Item.SetResearchAmount(1);
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