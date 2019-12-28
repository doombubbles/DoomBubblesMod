using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Basic
{
    public class AmplifyingTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases your magic power breifly");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.mana = 20;
            item.value = Item.buyPrice(0, 4, 35);
            item.rare = 1;
            item.buffTime = 60 * 20;
            item.buffType = BuffID.MagicPower;
        }
    }
}