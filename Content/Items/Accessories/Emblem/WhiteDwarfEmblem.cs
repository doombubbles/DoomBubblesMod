using DoomBubblesMod.Content.Items.Misc;

namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

public class WhiteDwarfEmblem : ModItem, IHasThoriumRecipe
{
    public override void SetStaticDefaults()
    {
        // Tooltip.SetDefault("20% increased throwing damage");
        Item.ResearchUnlockCount = 1;
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
        player.GetDamage(DamageClass.Throwing) += .2f;
    }

    public void AddThoriumRecipe(Mod thoriumMod) => CreateRecipe()
        .AddTile(TileID.LunarCraftingStation)
        .AddIngredient(thoriumMod.Find<ModItem>("NinjaEmblem"))
        .AddIngredient(ItemID.LunarBar, 5)
        .Register();
}