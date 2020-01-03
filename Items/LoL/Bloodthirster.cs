using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class Bloodthirster : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% Lifesteal\nYour Lifesteal limit is doubled");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.melee = true;
            item.width = 48;
            item.height = 48;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 35);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Thirster");
            item.shootSpeed = 10f;
            item.scale = 1.1f;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().lifesteal += .2f;
            player.GetModPlayer<DoomBubblesPlayer>().lifestealCap += 80f;
            player.GetModPlayer<DoomBubblesPlayer>().lifestealCapX += 70f;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().lifesteal += .2f;
            player.GetModPlayer<DoomBubblesPlayer>().lifestealCap += 80f;
            player.GetModPlayer<DoomBubblesPlayer>().lifestealCapX += 70f;
            base.HoldItem(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BFSword"));
            recipe.AddIngredient(mod.ItemType("VampiricScepter"));
            recipe.AddIngredient(mod.ItemType("LongSword"));
            recipe.AddIngredient(ItemID.GoldCoin, 9);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        

    }
}
