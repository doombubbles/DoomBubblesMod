using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]

public class CompuproHead : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Compupro Helmet");
        Tooltip.SetDefault("Great for impersonating compupro");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        var realSlot = Item.headSlot;
        Item.CloneDefaults(ItemID.TVHeadMask);
        Item.headSlot = realSlot;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<CompuPlayer>().showCompuGlow = true;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MeteorHelmet);
        recipe.AddIngredient(ItemID.CrystalShard, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}