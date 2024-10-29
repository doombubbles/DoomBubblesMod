using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

public class CrystalEmblem : ModItem
{
    public override void SetStaticDefaults()
    {
        /* Tooltip.SetDefault("15% increased ranged damage\n" +
                           "Crystal Bullets release extra shards"); */
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(0, 5);
        Item.width = 28;
        Item.height = 28;
        Item.rare = ItemRarityID.Pink;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Ranged) += .15f;
        player.GetModPlayer<DoomBubblesPlayer>().crystalBulletBonus = true;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AvengerEmblem);
        recipe.AddIngredient(ItemType<CrystalCore>());
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}