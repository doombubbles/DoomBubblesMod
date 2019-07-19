using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentPowerOverflowing : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Power Overflowing");
            Tooltip.SetDefault("Pylon Talent\n" +
                               "Your Pylons buff player damage by 15%\n" +
                               "[Right Click on a Pylon Staff with this to apply]");
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