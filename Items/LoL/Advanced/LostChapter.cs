﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class LostChapter : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Restores 20 mana and increases mana regeneration breifly\n" +
                               "Equipped - 30 maximum mana, 10% cooldown reduction and magic damage");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.value = Item.buyPrice(0, 13);
            item.rare = 1;
            item.buffTime = 60 * 20;
            item.buffType = BuffID.ManaRegeneration;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            player.statManaMax2 += 30;
            player.magicDamage += .1f;
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            item.accessory = true;
        }

        public override bool UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                player.statMana += 20;
            }
            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AmplifyingTome"));
            recipe.AddIngredient(mod.ItemType("SapphireCrystal"));
            recipe.AddIngredient(mod.ItemType("AmplifyingTome"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}