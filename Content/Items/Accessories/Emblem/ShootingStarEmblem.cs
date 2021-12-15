using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

internal class ShootingStarEmblem : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Shooting Star Emblem");
        Tooltip.SetDefault("20% increased symphonic damage");
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
        player.SymphonicDamage(f => f + .2f);
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
        recipe.AddIngredient(thoriumMod.Find<ModItem>("BardEmblem"));
        recipe.AddIngredient(thoriumMod.Find<ModItem>("CometFragment"), 5);
    }
}