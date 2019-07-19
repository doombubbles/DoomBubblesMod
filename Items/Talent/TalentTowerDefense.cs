using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentTowerDefense : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Tower Defense");
            Tooltip.SetDefault("Photon Cannon Talent\n" +
                               "Cannons only take up half a minion slot\n" +
                               "[Right Click on a Photon Cannon Staff with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 42;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42, 0, 0);
            item.rare = ItemRarityID.Yellow;
        }
    }
}