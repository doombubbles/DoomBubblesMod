using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentArsenalOvercharge : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Arsenal Overcharge");
            Tooltip.SetDefault("Repeater Cannon Talent\n" +
                               "Buff stacks up to 15 shots\n" +
                               "[Right Click on a Repeater Cannon with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42);
            item.rare = ItemRarityID.Orange;
        }
    }
}