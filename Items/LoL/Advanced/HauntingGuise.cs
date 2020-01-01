using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class HauntingGuise : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("5% increased magic damage\n" +
                               "Increases maximum life by 20\n" +
                               "Stack up to 10% further increased magic damage in combat");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 15);
            item.width = 28;
            item.height = 30;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().haunting += 2;
            player.statLifeMax2 += 20;
            player.magicDamage += .05f;
            if (player.HasBuff(mod.BuffType("Haunting")))
                player.magicDamage += .01f * (player.GetModPlayer<LoLPlayer>().haunting - 1);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RubyCrystal"));
            recipe.AddIngredient(mod.ItemType("AmplifyingTome"));
            recipe.AddIngredient(ItemID.GoldCoin, 6);
            recipe.AddIngredient(ItemID.SilverCoin, 65);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
    }
}