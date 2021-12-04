using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Talent;

public class TalentMobileOffense : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Mobile Offense");
        Tooltip.SetDefault("Repeater Cannon Talent\n" +
                           "Damage increases with player velocity\n" +
                           "[Right Click on a Repeater Cannon with this to apply]");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.maxStack = 1;
        Item.value = Item.buyPrice(0, 42);
        Item.rare = ItemRarityID.Orange;
    }
}