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
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 42);
            Item.rare = ItemRarityID.Lime;
        }
    }
}