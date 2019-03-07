using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    class Spirit : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 12;
            item.height = 12;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            ItemID.Sets.NebulaPickup[item.type] = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Spirit");
            Tooltip.SetDefault("");
        }

        public override bool CanPickup(Player player)
        {
            int count = 0;
            for (int i = 0; i < player.inventory.Length; i ++)
            {
                if (player.inventory[i].type == mod.ItemType("Frostmourne"))
                {
                    count += 1;
                }
            }
            return count > 0;
        }

        public override bool OnPickup(Player player)
        {
            Main.PlaySound(4, (int)player.position.X, (int)player.position.Y, 6, 0.5f);
            player.AddBuff(mod.BuffType("SpiritBuff"), 9999999);
            player.GetModPlayer<DoomBubblesPlayer>(mod).frostmourne += 1;
            return false;
        }
    }
}
