using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentConvection : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Convection");
            Tooltip.SetDefault("Flamestrike Talent\n" +
                               "Flamestrike hits give a stacking buff that's lost on death\n" +
                               "[Right Click on a Flamestrike Tome with this to apply]");
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