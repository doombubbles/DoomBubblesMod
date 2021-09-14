using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Thanos
{
    internal class TimeStone : InfinityStone
    {
        protected override int Rarity => ItemRarityID.Green;
        protected override int Gem => ItemID.Emerald;
        protected override Color Color => InfinityGauntlet.TimeColor;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Stone");
            Tooltip.SetDefault("\"Dormammu, I've come to bargain.\"\n" +
                               "-Doctor Strange, multiple occasions");
            Item.SetResearchAmount(1);
        }
    }
}