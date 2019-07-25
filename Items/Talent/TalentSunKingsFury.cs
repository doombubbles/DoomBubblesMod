using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentSunKingsFury : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Sun King's Fury");
            Tooltip.SetDefault("Living Bomb Talent\n" +
                               "Living Bombs deal more damage after spreading\n" +
                               "[Right Click on a Living Bomb Wand with this to apply]");
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