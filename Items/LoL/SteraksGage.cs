using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod.Items.LoL
{
    class SteraksGage : ModItem
    {
        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 32);
            item.width = 34;
            item.height = 36;
            item.rare = 8;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sterak's Gage");
            Tooltip.SetDefault( "Increases the base damage of your melee weapons by 10%\n" +
                                "Increases max life by 45\n" +
                                "Taking 25% of your current health activates Sterak's Fury");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 45;
            player.GetModPlayer<LoLPlayer>().sterak = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("JaurimsFist"));
            recipe.AddIngredient(mod.ItemType("PickaxeOfLegends"));
            recipe.AddIngredient(mod.ItemType("RubyCrystal"));
            recipe.AddIngredient(ItemID.GoldCoin, 7);
            recipe.AddIngredient(ItemID.SilverCoin, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}