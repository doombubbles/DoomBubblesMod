using DoomBubblesMod.Common.Configs;
using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Items.Accessories;

public class SorcerersStone : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Sorcerer's Stone");
        /* Tooltip.SetDefault(GetInstance<ServerConfig>().SorcerersStoneOP
            ? "Your health and mana always regenerate as if you weren't moving"
            : "Your mana always regenerates as if you weren't moving"); */
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.PhilosophersStone);
    }


    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<DoomBubblesPlayer>().sStone = true;
    }


    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PhilosophersStone);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();

        var recipe2 = CreateRecipe();
        recipe2.AddIngredient(this);
        recipe2.AddTile(TileID.AlchemyTable);
        recipe2.ReplaceResult(ItemID.PhilosophersStone);
        recipe2.Register();
    }
}