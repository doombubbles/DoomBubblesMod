using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

internal class HeavenlyEmblem : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Heavenly Emblem");
        Tooltip.SetDefault("20% increased radiant damage");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(0, 5);
        Item.width = 28;
        Item.height = 28;
        Item.rare = ItemRarityID.Red;
        Item.accessory = true;
    }


    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.RadiantDamage(f => f + .2f);
    }

    public override void AddRecipes()
    {
        if (DoomBubblesMod.ThoriumMod != null)
        {
            var recipe = CreateRecipe();
            AddThoriumRecipe(ref recipe);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.ReplaceResult(this);
            recipe.Register();
        }
    }

    public void AddThoriumRecipe(ref Recipe recipe)
    {
        var thoriumMod = DoomBubblesMod.ThoriumMod;
        recipe.AddIngredient(thoriumMod.Find<ModItem>("ClericEmblem"));
        recipe.AddIngredient(thoriumMod.Find<ModItem>("CelestialFragment"), 5);
    }
}