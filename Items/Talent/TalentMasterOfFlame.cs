using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentMasterOfFlame : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Master of Flame");
            Tooltip.SetDefault("Living Bomb Talent\n" +
                               "Living Bomb can spread indefinitely\n" +
                               "[Right Click on a Living Bomb Wand with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42, 0, 0);
            item.rare = ItemRarityID.Lime;
        }
    }
}