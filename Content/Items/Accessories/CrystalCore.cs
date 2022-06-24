using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Items.Accessories;

public class CrystalCore : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Crystal Core");
        Tooltip.SetDefault("Crystal Bullets release extra shards");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(0, 2);
        Item.width = 26;
        Item.height = 26;
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }


    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<DoomBubblesPlayer>().crystalBulletBonus = true;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CrystalShard, 25);
        recipe.AddIngredient(ItemID.SoulofLight, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}