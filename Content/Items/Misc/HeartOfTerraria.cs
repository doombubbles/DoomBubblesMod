using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Misc;

public class HeartOfTerraria : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Heart of Terraria");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 22;
        Item.maxStack = 99;
        Item.value = Item.sellPrice(0, 9, 99);
        Item.rare = ItemRarityID.Yellow;
    }
}