using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Talent;

public class TalentDivertPowerWeapons : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Divert Power - Weapons");
        Tooltip.SetDefault("Phase Bomb Launcher Talent\n" +
                           "Slower use time; Higher damage\n" +
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