using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod.Items.LoL
{
    class WarmogsArmor : ModItem
    {
        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 28, 50);
            item.width = 30;
            item.height = 32;
            item.rare = 8;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warmog's Armor");
            Tooltip.SetDefault("Increases max life by 80\n" +
                               "10% cooldown reduction and increased life regen\n" +
                               "Grants Warmog's Heart if you have more than 600 maximum life");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 80;
            player.lifeRegen += 4;
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            player.GetModPlayer<LoLPlayer>().warmogs = true;
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