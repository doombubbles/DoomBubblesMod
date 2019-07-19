using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentSingularityCharge : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Singularity Charge");
            Tooltip.SetDefault("Phase Bomb Launcher Talent\n" +
                               "Explosions that only hit 1 enemy deal bonus damage\n" +
                               "[Right Click on a Phase Bomb Launcher with this to apply]");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 42;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 42, 0, 0);
            item.rare = ItemRarityID.Orange;
        }
    }
}