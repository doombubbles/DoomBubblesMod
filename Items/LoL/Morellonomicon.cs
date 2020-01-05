using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL
{
    public class Morellonomicon : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases magic penetration by 15\n" +
                               "15% increased magic damage\n" +
                               "Equipped - Increases maximum life by 30");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 16);
            item.width = 28;
            item.height = 30;
            item.rare = 8;
            item.damage = 70;
            item.useTime = 7;
            item.useAnimation = 7;
            item.shoot = ProjectileID.ClothiersCurse;
            item.shootSpeed = 13;
            item.useStyle = 5;
            item.noMelee = true;
            item.magic = true;
            item.mana = 7;
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
            player.GetModPlayer<LoLPlayer>().magicPenetration += 15;
            player.magicDamage += .15f;
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 30;
            player.GetModPlayer<LoLPlayer>().magicPenetration += 15;
            player.magicDamage += .15f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("OblivionOrb"));
            recipe.AddIngredient(mod.ItemType("BlastingWand"));
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
    }
}