using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentPhotonicWeaponry : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Photonic Weaponry");
            Tooltip.SetDefault("Shield Capacitor Talent\n" +
                               "You deal 15% more damage while Shield Capacitor is active\n" +
                               "[Right Click on a Shield Capacitor with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42, 0, 0);
            item.rare = ItemRarityID.Orange;
        }
    }
}