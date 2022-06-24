using DoomBubblesMod.Content.Items.Misc;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

public class HeavenlyEmblem : ModItem, IHasThoriumRecipe
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("20% increased radiant damage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(0, 5);
        Item.width = 28;
        Item.height = 28;
        Item.rare = ItemRarityID.Red;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.RadiantDamage(f => f + .2f);
    }

    public void AddThoriumRecipe(Mod thoriumMod) => CreateRecipe()
        .AddTile(TileID.LunarCraftingStation)
        .AddIngredient(thoriumMod.Find<ModItem>("ClericEmblem"))
        .AddIngredient(thoriumMod.Find<ModItem>("CelestialFragment"), 5)
        .Register();
}