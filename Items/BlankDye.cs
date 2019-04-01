using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace DoomBubblesMod.Items
{
    public class BlankDye : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 24;
            item.maxStack = 99;
            item.value = Item.sellPrice(0, 0, 20, 0);
        }

        public override void AddRecipes()
        {
            List<int> dyes = new List<int>();
            for (int i = 1007; i <= 1070; i++)
            {
                dyes.Add(i);
            }
            for (int i = 2869; i <= 2879; i++)
            {
                dyes.Add(i);
            }
            for (int i = 2883; i <= 2885; i++)
            {
                dyes.Add(i);
            }
            for (int i = 3024; i <= 3028; i++)
            {
                dyes.Add(i);
            }
            for (int i = 3038; i <= 3042; i++)
            {
                dyes.Add(i);
            }
            for (int i = 3526; i <= 3530; i++)
            {
                dyes.Add(i);
            }
            for (int i = 3533; i <= 3535; i++)
            {
                dyes.Add(i);
            }
            for (int i = 3550; i <= 3562; i++)
            {
                dyes.Add(i);
            }
            for (int i = 3597; i <= 3600; i++)
            {
                dyes.Add(i);
            }
            
            dyes.ForEach(i =>
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(i);
                recipe.AddIngredient(mod.ItemType("BlankDye"));
                recipe.SetResult(i, 2);
                recipe.AddTile(TileID.DyeVat);
                recipe.AddRecipe();
            });
        }
    }
}