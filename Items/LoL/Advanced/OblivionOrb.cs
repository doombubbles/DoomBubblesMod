using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class OblivionOrb : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("5% increased magic damage\n" +
                               "Increases maximum life by 20\n" +
                               "Increases magic penetration by 15");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 16);
            item.width = 28;
            item.height = 28;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.GetModPlayer<LoLPlayer>().magicPenetration += 15;
            player.magicDamage += .05f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RubyCrystal"));
            recipe.AddIngredient(mod.ItemType("AmplifyingTome"));
            recipe.AddIngredient(ItemID.GoldCoin, 7);
            recipe.AddIngredient(ItemID.SilverCoin, 65);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
    }
}