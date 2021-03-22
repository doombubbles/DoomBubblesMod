using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentShootEmUp : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Shoot Em Up");
            Tooltip.SetDefault("Photon Cannon Talent\n" +
                               "Cannons attack twice as fast\n" +
                               "[Right Click on a Photon Cannon Staff with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42);
            item.rare = ItemRarityID.Yellow;
        }
    }
}