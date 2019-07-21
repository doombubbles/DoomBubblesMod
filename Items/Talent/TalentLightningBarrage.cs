using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentLightningBarrage : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Lightning Barrage");
            Tooltip.SetDefault("Lightning Surge Talent\n" +
                               "Can zap up to 2 enemies at once\n" +
                               "[Right Click on a Lightning Surge with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42, 0, 0);
            item.rare = ItemRarityID.Red;
        }
    }
}