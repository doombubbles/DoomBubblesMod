using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Talent;

public class TalentSunKingsFury : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Sun King's Fury");
        Tooltip.SetDefault("Living Bomb Talent\n" +
                           "Living Bombs deal more damage after spreading\n" +
                           "[Right Click on a Living Bomb Wand with this to apply]");
        Item.SetResearchAmount(1);
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