namespace DoomBubblesMod.Content.Items.Accessories;

public class RoyalHoney : ModItem
{
    public override void SetDefaults()
    {
        Item.value = 42069;
        Item.width = 20;
        Item.height = 30;
        Item.rare = -12;
        Item.accessory = true;
    }

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Royal Honey");
        Tooltip.SetDefault("Bees are friendly");
        SacrificeTotal = 1;
    }


    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.npcTypeNoAggro[NPCID.Bee] = true;
        player.npcTypeNoAggro[NPCID.BeeSmall] = true;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BottledHoney);
        recipe.AddIngredient(ItemID.GoldCrown);
        recipe.AddIngredient(ItemID.RoyalGel);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();

        var recipe2 = CreateRecipe();
        recipe2.AddIngredient(ItemID.BottledHoney);
        recipe2.AddIngredient(ItemID.PlatinumCrown);
        recipe2.AddIngredient(ItemID.RoyalGel);
        recipe2.AddTile(TileID.DemonAltar);
        recipe2.Register();
    }
}