using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Thanos
{
    internal class MindStone : InfinityStone
    {
        protected override int Rarity => ItemRarityID.Yellow;
        protected override int Gem => ItemID.Topaz;
        protected override Color Color => InfinityGauntlet.MindColor;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mind Stone");
            Tooltip.SetDefault("\"The Mind Stone is the fourth of the Infinity Stones to show up in the last\n" +
                               "few years. It's not a coincidence. Someone has been playing an intricate\n" +
                               "game and has made pawns of us.\"\n" +
                               "-Thor");
            Item.SetResearchAmount(1);
        }
    }
}