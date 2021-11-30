using DoomBubblesMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Talent
{
    public class TalentMasterOfFlame : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talent: Master of Flame");
            Tooltip.SetDefault("Living Bomb Talent\n" +
                               "Living Bomb can spread indefinitely\n" +
                               "[Right Click on a Living Bomb Wand with this to apply]");
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