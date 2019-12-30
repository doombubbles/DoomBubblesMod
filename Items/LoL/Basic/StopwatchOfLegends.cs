using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Basic
{
    public class StopwatchOfLegends : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Put yourself in stasis for 2.5 seconds");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.useStyle = 5;
            item.useTime = 20;
            item.useAnimation = 20;
            item.value = Item.buyPrice(0, 6);
            item.rare = 1;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().stasis = 30 * 5;
            player.GetModPlayer<LoLPlayer>().stasisLife = player.statLife;
            player.GetModPlayer<LoLPlayer>().stasisMana = player.statMana;
            player.GetModPlayer<LoLPlayer>().stasisX = player.position.X;
            player.GetModPlayer<LoLPlayer>().stasisY = player.position.Y;
            return true;
        }
    }
}