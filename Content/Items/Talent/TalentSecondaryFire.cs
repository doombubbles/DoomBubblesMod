using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Talent;

public class TalentSecondaryFire : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Secondary Fire");
        Tooltip.SetDefault("Phase Bomb Launcher Talent\n" +
                           "Bombs also hit enemies they pass through\n" +
                           "[Right Click on a Phase Bomb Launcher with this to apply]");
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