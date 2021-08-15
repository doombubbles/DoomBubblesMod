using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentWarpResonance : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Warp Resonance");
            Tooltip.SetDefault("Photon Cannon Talent\n" +
                               "Cannons no longer require Pylon Power, but are buffed by it\n" +
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