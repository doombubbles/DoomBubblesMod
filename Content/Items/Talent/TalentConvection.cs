using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Talent;

public class TalentConvection : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Convection");
        Tooltip.SetDefault("Flamestrike Talent\n" +
                           "Flamestrike hits give a stacking buff that's lost on death\n" +
                           "[Right Click on a Flamestrike Tome with this to apply]");
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