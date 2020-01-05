using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class IcebornGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("After hitting with a projectile, your next swing makes an icy zone\n" +
                               "Increased maximum mana by 50 and 20% cooldown reduction");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.buyPrice(0, 27);
            item.rare = 8;
            item.defense = 8;
            item.accessory = true;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().iceborn = true;
            player.statManaMax2 += 50;
            player.GetModPlayer<LoLPlayer>().cdr += .2f;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Sheen"));
            recipe.AddIngredient(mod.ItemType("GlacialShroud"));
            recipe.AddIngredient(ItemID.GoldCoin, 7);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}