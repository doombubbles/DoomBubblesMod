using DoomBubblesMod.Content.Items.Accessories;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]

public class CompuproHead : ModItem
{
    public static int headSlot;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Compupro's Terminal");
        Tooltip.SetDefault("'Great for impersonating compupro!'\n" +
                           "If you look deep into its eyes, you can just about make out the words...\n" +
                           "'Civilization V has crashed.'");
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
        recipe.AddIngredient(ItemType<FredericksGift>());
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}