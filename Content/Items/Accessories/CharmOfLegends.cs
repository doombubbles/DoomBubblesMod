using DoomBubblesMod.Common.Configs;
using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.HandsOn)]
public class CharmOfLegends : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Charm of Epics");
        /* Tooltip.SetDefault("Reduces the cooldown of healing potions\n" +
                           (GetInstance<ServerConfig>().SorcerersStoneOP
                               ? "Health/mana always regenerate as if you weren't moving\n"
                               : "Your mana always regenerates as if you weren't moving") +
                           "Incrases maximum mana by 20\n" +
                           "Increases mana and life regeneration rate"); */
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        var real = Item.handOnSlot;
        Item.CloneDefaults(ItemID.CharmofMyths);
        Item.handOnSlot = real;
    }


    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<DoomBubblesPlayer>().sStone = true;
        player.statManaMax2 += 20;
        player.manaRegenDelayBonus++;
        player.manaRegenBonus += 25;
        player.lifeRegen++;
        player.pStone = true;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CharmofMyths);
        recipe.AddIngredient(ItemType<CharmOfFables>());
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}