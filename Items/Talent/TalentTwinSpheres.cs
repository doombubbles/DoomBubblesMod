using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentTwinSpheres : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Twin Spheres");
            Tooltip.SetDefault("Verdant Spheres Talent\n" +
                               "Base Verdant Sphere bonuses are doubled\n" +
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