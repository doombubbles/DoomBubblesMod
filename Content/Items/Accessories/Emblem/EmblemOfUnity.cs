using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Misc;

namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

public class EmblemOfUnity : ModItem, IHasThoriumRecipe
{
    public override void SetStaticDefaults()
    {
        /* Tooltip.SetDefault("10% increased damage\n" +
                           "+10% damage for each other player wearing this"); */
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 28;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = Item.sellPrice(0, 8);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<DoomBubblesPlayer>().united = true;
    }

    public void AddThoriumRecipe(Mod thoriumMod)
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(thoriumMod.Find<ModItem>("RingofUnity"));
        recipe.AddIngredient(ItemID.AvengerEmblem);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}