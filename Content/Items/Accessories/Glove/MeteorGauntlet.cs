using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Glove;

internal class MeteorGauntlet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Meteor Gauntlet");
        Tooltip.SetDefault("5% increased attack speed");
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
        player.AttackSpeed(f => f + .05f);
    }

    public override void AddRecipes()
    {
        if (DoomBubblesMod.ThoriumMod != null)
        {
            addThoriumRecipe();
        }
    }

    private void addThoriumRecipe()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(DoomBubblesMod.ThoriumMod.Find<ModItem>("LeatherGlove"));
        recipe.AddIngredient(ItemID.MeteoriteBar, 7);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}