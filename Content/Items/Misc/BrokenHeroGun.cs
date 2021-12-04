using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Misc;

public class BrokenHeroGun : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Broken Hero Gun");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 26;
        Item.maxStack = 99;
        Item.value = Item.sellPrice(0, 2);
        Item.rare = ItemRarityID.Yellow;
    }

    public override void AddRecipes()
    {
        if (DoomBubblesMod.ThoriumMod != null)
        {
            ThoriumRecipes();
        }
    }

    private void ThoriumRecipes()
    {
        var recipe1 = CreateRecipe();
        recipe1.AddIngredient(this);
        recipe1.ReplaceResult(DoomBubblesMod.ThoriumMod.Find<ModItem>("BrokenHeroFragment"), 2);
        recipe1.AddTile(TileID.MythrilAnvil);
        recipe1.Register();

        var recipe2 = CreateRecipe();
        recipe2.ReplaceResult(this);
        recipe2.AddIngredient(DoomBubblesMod.ThoriumMod.Find<ModItem>("BrokenHeroFragment"), 2);
        recipe2.AddTile(TileID.MythrilAnvil);
        recipe2.Register();

        var recipe3 = CreateRecipe();
        recipe3.AddIngredient(ItemID.BrokenHeroSword);
        recipe3.ReplaceResult(DoomBubblesMod.ThoriumMod.Find<ModItem>("BrokenHeroFragment"), 2);
        recipe3.AddTile(TileID.MythrilAnvil);
        recipe3.Register();

        var recipe4 = CreateRecipe();
        recipe4.ReplaceResult(ItemID.BrokenHeroSword);
        recipe4.AddIngredient(DoomBubblesMod.ThoriumMod.Find<ModItem>("BrokenHeroFragment"), 2);
        recipe4.AddTile(TileID.MythrilAnvil);
        recipe4.Register();
    }
}