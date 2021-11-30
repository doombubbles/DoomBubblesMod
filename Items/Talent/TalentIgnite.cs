using DoomBubblesMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentIgnite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Ignite");
            Tooltip.SetDefault("Flamestrike Talent\n" +
                               "Flamestrike applies Living Bomb to 1 enemy\n" +
                               "[Right Click on a Flamestrike Tome with this to apply]");
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