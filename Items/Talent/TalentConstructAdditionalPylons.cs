using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentConstructAdditionalPylons : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Construct Additional Pylons");
            Tooltip.SetDefault("Pylon Talent\n" +
                               "Up to 3 Pylons with bigger range\n" +
                               "[Right Click on a Pylon Staff with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42, 0, 0);
            item.rare = ItemRarityID.Yellow;
        }
    }
}