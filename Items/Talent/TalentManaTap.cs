using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentManaTap : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Mana Tap");
            Tooltip.SetDefault("Verdant Spheres Talent\n" +
                               "Magic Weapons' base mana cost is added to their damage\n" +
                               "[Right Click on a Verdant Spheres with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42, 0, 0);
            item.rare = ItemRarityID.Lime;
        }
    }
}