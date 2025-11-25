namespace DoomBubblesMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Balloon)]
public class BigBadBundle : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Big Bad Blessed Bundle of Balloons");
        // Tooltip.SetDefault("Allows the holder to septuple jump\nReleases bees when damaged");
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.value = 100000;
        Item.width = 42;
        Item.height = 42;
        Item.rare = ItemRarityID.LightPurple;
        Item.accessory = true;
    }


    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetJumpState(ExtraJump.BlizzardInABottle).Enable();
        player.GetJumpState(ExtraJump.CloudInABottle).Enable();
        player.GetJumpState(ExtraJump.FartInAJar).Enable();
        player.GetJumpState(ExtraJump.SandstormInABottle).Enable();
        player.GetJumpState(ExtraJump.TsunamiInABottle).Enable();
        player.GetJumpState(ExtraJump.UnicornMount).Enable();
        player.honeyCombItem = Item;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BundleofBalloons);
        recipe.AddIngredient(ItemID.FartInABalloon);
        recipe.AddIngredient(ItemID.HoneyBalloon);
        recipe.AddIngredient(ItemID.SharkronBalloon);
        recipe.AddIngredient(ItemID.BlessedApple);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}