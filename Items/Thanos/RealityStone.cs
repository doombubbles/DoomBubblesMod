using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Thanos
{
    internal class RealityStone : InfinityStone
    {
        protected override int Rarity => ItemRarityID.Red;
        protected override int Gem => ItemID.Ruby;
        protected override Color Color => InfinityGauntlet.RealityColor;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reality Stone");
            Tooltip.SetDefault("\"Long before the birth of light there was darkness, and from that darkness\n" +
                               "came the Dark Elves. Millennia ago the most ruthless of their\n" +
                               "kind, Malekith, sought to transform our universe back into one of eternal\n" +
                               "night. Such evil was possible through the power of the Aether, an ancient\n" +
                               "force of infinite destruction.\"\n" +
                               "-Odin");
            Item.SetResearchAmount(1);
        }
    }
}