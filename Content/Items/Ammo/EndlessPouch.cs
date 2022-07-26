using System.Text.RegularExpressions;

namespace DoomBubblesMod.Content.Items.Ammo;

public abstract class EndlessPouch : ModItem
{
    protected abstract int AmmunitionItem { get; }

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault($"Endless {Regex.Replace(Name, "([A-Z])", " $1").Trim()}");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(AmmunitionItem);
        Item.width = 26;
        Item.height = 26;
        Item.consumable = false;
        Item.maxStack = 1;
        Item.value *= 3996;
        Item.rare += 1;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(AmmunitionItem, 3996)
            .AddTile(TileID.CrystalBall)
            .Register();
    }
}

public abstract class EndlessPouch<T> : EndlessPouch where T : ModItem
{
    protected override int AmmunitionItem => ItemType<T>();
}