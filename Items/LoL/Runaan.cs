using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    class Runaan : ModItem
    {
        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 26);
            item.rare = 8;
            item.useStyle = 5;
            item.useAnimation = 15;
            item.width = 22;
            item.height = 44;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.damage = 47;
            item.crit = 21;
            item.shootSpeed = 10.0f;
            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;
            item.useTime = 15;
            item.UseSound = SoundID.Item5;
            item.knockBack = 2;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().runaan = true;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().runaan = true;
            base.HoldItem(player);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Runaan's Hurricane");
            Tooltip.SetDefault("Shoots additional homing bolts");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Dagger"));
            recipe.AddIngredient(mod.ItemType("Zeal"));
            recipe.AddIngredient(mod.ItemType("Dagger"));
            recipe.AddIngredient(ItemID.GoldCoin, 6);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(5, 0);
        }
    }
}