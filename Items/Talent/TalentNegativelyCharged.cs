using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentNegativelyCharged : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Negatively Charged");
            Tooltip.SetDefault("Lightning Surge Talent\n" +
                               "Further increased center damage bonus\n" +
                               "[Right Click on a Lightning Surge with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 42;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42, 0, 0);
            item.rare = ItemRarityID.Red;
        }
    }
}