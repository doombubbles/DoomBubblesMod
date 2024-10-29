namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

public class TimsRegret : ModItem
{
    private static float Factor => ThoriumMod != null ? .2f : .15f;

    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Tim's Regret");
        // Tooltip.SetDefault($"{Factor:P0} increased damage\n100% increased n00b regret");
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(0, 10);
        Item.width = 28;
        Item.height = 28;
        Item.rare = ItemRarityID.LightPurple;
        Item.accessory = true;
    }


    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += Factor;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WarriorEmblem);
        recipe.AddIngredient(ItemID.SorcererEmblem);
        recipe.AddIngredient(ItemID.RangerEmblem);
        recipe.AddIngredient(ItemID.SummonerEmblem);

        if (ThoriumMod != null)
        {
            recipe.AddIngredient(ThoriumMod.Find<ModItem>("BardEmblem"));
            recipe.AddIngredient(ThoriumMod.Find<ModItem>("NinjaEmblem"));
            recipe.AddIngredient(ThoriumMod.Find<ModItem>("ClericEmblem"));
        }

        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}