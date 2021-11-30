using System;
using DoomBubblesMod.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Thanos
{
    internal class SoulStone : InfinityStone
    {
        protected override int Rarity => ItemRarityID.Quest;
        protected override int Gem => ItemID.Amber;
        protected override Color Color => InfinityGauntlet.SoulColor;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Stone");
            Tooltip.SetDefault("\"Soul holds a special place among the Infinity Stones.\n" +
                               "You might say, it is a certain wisdom. To ensure that\n" +
                               "whoever possesses it understands its power, the stone\n" +
                               "demands a sacrifice. In order to take the stone, you\n" +
                               "must lose that which you love. A soul for a soul.\"\n" +
                               "-The Stonekeeper");
            Item.SetResearchAmount(1);
        }
    }
}