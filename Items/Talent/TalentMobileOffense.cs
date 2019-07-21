using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentMobileOffense : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Mobile Offense");
            Tooltip.SetDefault("Repeater Cannon Talent\n" +
                               "Damage increases with player velocity\n" +
                               "[Right Click on a Repeater Cannon with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42, 0, 0);
            item.rare = ItemRarityID.Orange;
        }
    }
}