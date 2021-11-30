using System;
using DoomBubblesMod.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Thanos
{
    internal class PowerStone : InfinityStone
    {
        protected override int Rarity => ItemRarityID.Purple;
        protected override int Gem => ItemID.Amethyst;
        protected override Color Color => InfinityGauntlet.PowerColor;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power Stone");
            Tooltip.SetDefault("\"The stone reacts to anything organic, the bigger\n" +
                               "the target, the bigger the power surge.\"\n" +
                               "-Gamora");
            
            Item.SetResearchAmount(1);
        }
    }
}