using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentDivertPowerWeapons : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Divert Power - Weapons");
            Tooltip.SetDefault("Phase Bomb Launcher Talent\n" +
                               "Slower use time; Higher damage\n" +
                               "[Right Click on a Phase Bomb Launcher with this to apply]");
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