﻿using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;

namespace DoomBubblesMod.Items
{
    public class AngusBargain : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angus' Bargain");
            Tooltip.SetDefault("Gives 100k Gold on use");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 36;
            Item.maxStack = 20;
            Item.value = 0;
            Item.rare = ItemRarityID.Yellow;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return Main.npc.All(npc => npc.type != NPCID.DD2Betsy);
        }

        public override bool? UseItem(Player player)
        {
            if (CanUseItem(player))
            {
                SoundEngine.PlaySound(SoundID.Roar, player.Center, 0);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2Betsy);
                
                Main.NewText($"Whoops, {player.name} wasn't B-Hopping");
                
                return true;
            }
            return base.UseItem(player);
        }
    }
}