using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class AetherWisp : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased magic damage\n" +
                               "5% increased movement speed\n");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 8, 50);
            item.width = 22;
            item.height = 34;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += .1f;
            player.moveSpeed += .05f;
            player.maxRunSpeed += .5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AmplifyingTome"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddIngredient(ItemID.SilverCoin, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
    }
}