namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

public class StardustEmblem : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("20% increased summon damage");
        SacrificeTotal = 1;
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
        player.GetDamage(DamageClass.Summon) += .2f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SummonerEmblem);
        recipe.AddIngredient(ItemID.FragmentStardust, 5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}