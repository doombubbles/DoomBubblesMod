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
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 42);
            Item.rare = ItemRarityID.Orange;
        }
    }
}