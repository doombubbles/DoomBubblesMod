using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentDissonance : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Dissonance");
            Tooltip.SetDefault("Discord Strike Talent\n" +
                               "Discord Strike has signficantly longer range\n" +
                               "[Right Click on a Discord Blade with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 42;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42, 0, 0);
            item.rare = ItemRarityID.Red;
        }
    }
}