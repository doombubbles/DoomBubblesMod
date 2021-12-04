using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Utils;
using Terraria.ID;

namespace DoomBubblesMod.Content.Items.Accessories;

internal class CrystalCore : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Crystal Core");
        Tooltip.SetDefault("Crystal Bullets release extra shards");
        Item.SetResearchAmount(1);
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
        recipe.AddIngredient(ItemID.CrystalShard, 100);
        recipe.AddIngredient(ItemID.SoulofNight, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}