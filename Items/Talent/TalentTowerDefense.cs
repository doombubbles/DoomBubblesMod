using DoomBubblesMod.Utils;
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
}