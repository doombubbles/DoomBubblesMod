using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentSustainingPower : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Sustaining Power");
            Tooltip.SetDefault("Lightning Surge Talent\n" +
                               "Heal for enemies hit other than the target\n" +
                               "[Right Click on a Lightning Surge with this to apply]");
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