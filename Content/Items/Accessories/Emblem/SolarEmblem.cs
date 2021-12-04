using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

internal class SolarEmblem : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Solar Emblem");
        Tooltip.SetDefault("20% increased melee damage");
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
        player.GetDamage(DamageClass.Melee) += .2f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WarriorEmblem);
        recipe.AddIngredient(ItemID.FragmentSolar, 5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}