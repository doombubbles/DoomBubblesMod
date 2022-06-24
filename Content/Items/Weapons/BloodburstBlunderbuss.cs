using System;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Items.Weapons;

public class BloodburstBlunderbuss : NightsEdgeGun
{
    public override void SetStaticDefaults()
    {
        //Tooltip.SetDefault("This is a modded gun.");
        DisplayName.SetDefault("Bloodburst Blunderbuss");
        SacrificeTotal = 1;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TheUndertaker);
        recipe.AddIngredient(ItemID.Boomstick);
        recipe.AddIngredient(ItemID.PhoenixBlaster);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();

        var recipe2 = CreateRecipe();
        recipe2.AddIngredient(ItemType<MidnightMaelstrom>());
        recipe2.AddTile(TileID.DemonAltar);
        recipe2.Register();
    }
}