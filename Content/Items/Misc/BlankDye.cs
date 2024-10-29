using System;
using Terraria.Graphics.Shaders;

namespace DoomBubblesMod.Content.Items.Misc
{
    public class BlankDye : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 24;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 1);
        }

        public override void AddRecipes()
        {
            for (var i = 0; i < ItemLoader.ItemCount; i++)
            {
                if (i != ItemID.ColorOnlyDye && GameShaders.Armor.GetShaderIdFromItemId(i) != 0)
                {
                    var recipe = CreateRecipe();
                    recipe.AddIngredient(i);
                    recipe.AddIngredient(this);
                    recipe.ReplaceResult(i, 2);
                    recipe.AddTile(TileID.DyeVat);
                    try
                    {
                        recipe.Register();
                    }
                    catch (Exception e)
                    {
                        Mod.Logger.Warn($"Item with id {i} caused");
                        Mod.Logger.Warn(e);
                    }
                }
            }
        }
    }
}