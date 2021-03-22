using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentUnconqueredSpirit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Unconquered Spirit");
            Tooltip.SetDefault("Shield Capacitor Talent\n" +
                               "Upon taking fatal damage, prevent it and refresh Shield Capacitor. 2 min cooldown.\n" +
                               "[Right Click on a Shield Capacitor with this to apply]");
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