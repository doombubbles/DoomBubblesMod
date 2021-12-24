using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]

public class CompuproHead : ModItem
{
    public static int headSlot;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Compupro Helmet");
        Tooltip.SetDefault("Great for impersonating compupro");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        var realSlot = Item.headSlot;
        headSlot = realSlot;
        Item.CloneDefaults(ItemID.TVHeadMask);
        Item.headSlot = realSlot;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TVHeadMask);
        recipe.AddIngredient(ModContent.ItemType<Items.Accessories.FredericksGift>());
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}