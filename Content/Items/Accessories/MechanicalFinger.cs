using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories;

public class MechanicalFinger : ModItem
{
    public override void SetStaticDefaults()
    {
        /* Tooltip.SetDefault("12% increased ranged damage\n" +
                           "Enables auto shoot for ranged weapons\n"); */
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 10;
        Item.height = 10;
        Item.value = Item.sellPrice(0, 5);
        Item.accessory = true;
        Item.rare = ItemRarityID.LightPurple;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDoomBubblesPlayer().autoShoot = true;
        player.GetDamage(DamageClass.Ranged) += .12f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AvengerEmblem);
        recipe.AddIngredient<TriggerFinger>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}