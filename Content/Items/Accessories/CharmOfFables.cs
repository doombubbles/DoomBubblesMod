using DoomBubblesMod.Common.Configs;
using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.HandsOn)]
public class CharmOfFables : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Charm of Fables");
        Tooltip.SetDefault((GetInstance<ServerConfig>().SorcerersStoneOP
                               ? "Health/mana always regenerate as if you weren't moving\n"
                               : "Your mana always regenerates as if you weren't moving") +
                           "Incrases maximum mana by 20\n" +
                           "Increases mana regeneration rate");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        var real = Item.handOnSlot;
        Item.CloneDefaults(ItemID.CharmofMyths);
        Item.handOnSlot = real;
        Item.lifeRegen = 0;
    }


    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<DoomBubblesPlayer>().sStone = true;
        player.statManaMax2 += 20;
        player.manaRegenDelayBonus++;
        player.manaRegenBonus += 25;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemType<SorcerersStone>());
        recipe.AddIngredient(ItemID.ManaRegenerationBand);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}