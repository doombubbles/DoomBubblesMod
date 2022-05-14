using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Accessories.Glove;
using DoomBubblesMod.Content.Items.Misc;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

public class ChromaticEmblem : ThoriumRecipeItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Chromatic Emblem");
        Tooltip.SetDefault("15% increased damage\n" +
                           "15% increased crit chance\n" +
                           "15% increased attack speed\n" +
                           "15% reduced damage taken\n" +
                           "Increases armor penetration by 15\n" +
                           "Gravity Globe benefits ;)\n" +
                           "Can't be equipped with any other 'Emblems'");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(1);
        Item.width = 28;
        Item.height = 28;
        Item.rare = -12;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += .15f;
        player.GetCritChance(DamageClass.Generic) += 15;
        player.GetArmorPenetration(DamageClass.Generic) += 15;
        player.GetAttackSpeed(DamageClass.Generic) += .15f;
        player.endurance += .15f;

        player.gravControl2 = true;
        player.gravControl = true;
        player.GetDoomBubblesPlayer().emblem = -42;
    }

    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {
        return player.GetModPlayer<DoomBubblesPlayer>().emblem == 0 && base.CanEquipAccessory(player, slot, modded);
    }

    public override void AddThoriumRecipe(Mod thoriumMod)
    {
        var recipe = CreateRecipe();
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.AddIngredient(ItemType<TimsRegret>());
        recipe.AddIngredient(ItemType<ChromaticGauntlet>());
        recipe.AddIngredient(thoriumMod.Find<ModItem>("TheRing"));
        recipe.AddIngredient(ItemType<NebulaEmblem>());
        recipe.AddIngredient(ItemType<SolarEmblem>());
        recipe.AddIngredient(ItemType<StardustEmblem>());
        recipe.AddIngredient(ItemType<VortexEmblem>());
        recipe.AddIngredient(ItemType<ShootingStarEmblem>());
        recipe.AddIngredient(ItemType<HeavenlyEmblem>());
        recipe.AddIngredient(ItemType<WhiteDwarfEmblem>());
        recipe.AddIngredient(ItemID.GravityGlobe);
        recipe.Register();
    }
}