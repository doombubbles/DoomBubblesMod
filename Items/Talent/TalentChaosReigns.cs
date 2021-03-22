using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentChaosReigns : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Chaos Reigns");
            Tooltip.SetDefault("Discord Strike Talent\n" +
                               "Discord Strike does increasing damage as it passes through enemies\n" +
                               "[Right Click on a Discord Blade with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42);
            item.rare = ItemRarityID.Red;
        }
    }
}