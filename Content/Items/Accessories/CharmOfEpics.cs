using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.HandsOn)]
public class CharmOfEpics : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Charm of Legends");
        Tooltip.SetDefault("Reduces the cooldown of healing potions\n" +
                           "Incrases maximum mana by 20\n" +
                           "Improves life/mana regen in many ways\n" +
                           "Increases heart/star pickup range\n" +
                           "Regenerate mana on taking damage; health on dealing");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        var real = Item.handOnSlot;
        Item.CloneDefaults(ItemID.CharmofMyths);
        Item.handOnSlot = real;
        Item.value = Item.sellPrice(0, 10);
    }


    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<DoomBubblesPlayer>().sStone = true;
        player.statManaMax2 += 20;
        player.manaRegenDelayBonus++;
        player.manaRegenBonus += 25;
        player.lifeRegen++;
        player.pStone = true;
        player.crimsonRegen = true;
        player.shinyStone = true;
        player.lifeMagnet = true;
        player.manaMagnet = true;
        player.magicCuffs = true;
        player.onHitRegen = true;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemType<CharmOfLegends>());
        recipe.AddIngredient(ItemID.ShinyStone);
        if (SetBonusAccessories is Mod mod)
        {
            recipe.AddIngredient(mod, "Palladium");
            recipe.AddIngredient(mod, "Crimson");
        }
        else
        {
            recipe.AddIngredient(ItemType<CrimsonVoodooDoll>());
            recipe.AddIngredient(ItemType<PalladiumVoodooDoll>());
        }
        recipe.AddIngredient(ItemID.CelestialCuffs);
        recipe.AddIngredient(ItemType<CardiopulmonaryMagnet>());
        recipe.AddIngredient(ItemID.GravityGlobe);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}