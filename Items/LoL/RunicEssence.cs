using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    class RunicEssence : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Ichor);

            item.value = 100000;

            item.rare = 7;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Runic Essence");
      Tooltip.SetDefault("Far more useful than blue or orange essence");
    }

    }
}
