using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentDampeningField : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Dampening Field");
            Tooltip.SetDefault("Shield Capacitor Talent\n" +
                               "You take 25% less damage while Shield Capacitor is above half charge\n" +
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