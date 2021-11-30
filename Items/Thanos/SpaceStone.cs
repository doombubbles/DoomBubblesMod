using System;
using DoomBubblesMod.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Thanos
{
    internal class SpaceStone : InfinityStone
    {
        protected override int Rarity => ItemRarityID.Blue;
        protected override int Gem => ItemID.Sapphire;
        protected override Color Color => InfinityGauntlet.SpaceColor;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Space Stone");
            Tooltip.SetDefault("\"A lifetime ago, I too sought the stones. I even held one in my hand.\"\n" +
                               "-Red Skull to Thanos");
            Item.SetResearchAmount(1);
        }
    }
}