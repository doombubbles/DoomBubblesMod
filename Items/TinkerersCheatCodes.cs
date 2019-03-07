using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod.Items
{
    class TinkerersCheatCodes : ModItem
    {
        public override void SetDefaults()
        {

            item.value = 42069;
            item.width = 30;
            item.height = 30;
            item.rare = -12;
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tinkerer's Cheat Codes");
            Tooltip.SetDefault("It's literally just paying him more\n" +
                               "What were you expecting, the Konami Code?");
        }


        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<DoomBubblesPlayer>(mod).reforgeCheatCodes = true;
            base.UpdateInventory(player);
        }
    }
}
