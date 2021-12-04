using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

internal class NebulaEmblem : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Nebula Emblem");
        Tooltip.SetDefault("20% increased magic damage");
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
        player.GetDamage(DamageClass.Magic) += .20f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SorcererEmblem);
        recipe.AddIngredient(ItemID.FragmentNebula, 5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}