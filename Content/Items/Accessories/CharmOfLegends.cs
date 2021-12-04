using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.HandsOn)]
internal class CharmOfLegends : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Charm of Epics");
        Tooltip.SetDefault("Reduces the cooldown of healing potions\n" +
                           "Health/mana always regenerates as if you weren't moving\n" +
                           "Incrases maximum mana by 20\n" +
                           "Increases mana and life regeneration rate");
        Item.SetResearchAmount(1);
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
        recipe.AddIngredient(ModContent.ItemType<CharmOfFables>());
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}