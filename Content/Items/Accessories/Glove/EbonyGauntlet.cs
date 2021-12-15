﻿using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Glove;

internal class EbonyGauntlet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ebony Gauntlet");
        Tooltip.SetDefault("Increases armor penetration by 5");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(0, 1);
        Item.width = 36;
        Item.height = 40;
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.armorPenetration += 5;
    }

    public override void AddRecipes()
    {
        if (DoomBubblesMod.ThoriumMod != null)
        {
            AddThoriumRecipe();
        }
    }

    private void AddThoriumRecipe()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(DoomBubblesMod.ThoriumMod.Find<ModItem>("LeatherGlove"));
        recipe.AddIngredient(DoomBubblesMod.ThoriumMod.Find<ModItem>("Onyx"), 7);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}