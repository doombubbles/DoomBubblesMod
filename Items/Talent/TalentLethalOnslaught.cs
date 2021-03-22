using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentLethalOnslaught : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Lethal Onslaught");
            Tooltip.SetDefault("Discord Strike Talent\n" +
                               "Discord Strike does bonus damage to low health enemies\n" +
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