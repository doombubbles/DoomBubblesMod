using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Items.Accessories;

public class ExplosiveCharm : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Explosive Charm");
        Tooltip.SetDefault("Explosive Bullets no longer hurt you");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(0, 2);
        Item.width = 28;
        Item.height = 26;
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }


    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<DoomBubblesPlayer>().explosionBulletBonus = true;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ExplosivePowder, 100);
        recipe.AddIngredient(ItemID.SoulofNight, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}