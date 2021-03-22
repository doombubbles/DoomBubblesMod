using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentFelInfusion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Fel Infusion");
            Tooltip.SetDefault("Verdant Spheres Talent\n" +
                               "10% reduced damage; 25% increased magic damage\n" +
                               "[Right Click on a Verdant Spheres with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42);
            item.rare = ItemRarityID.Lime;
        }
    }
}