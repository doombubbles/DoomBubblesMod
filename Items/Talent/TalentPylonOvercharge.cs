using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentPylonOvercharge : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Pylon Overcharge");
            Tooltip.SetDefault("Pylon Talent\n" +
                               "Your Pylons attack shit now\n" +
                               "[Right Click on a Pylon Staff with this to apply]");
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