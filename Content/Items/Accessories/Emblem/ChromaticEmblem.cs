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
        player.endurance += .15f;
        player.armorPenetration += 15;
        player.AttackSpeed(f => f + .15f);

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
        recipe.AddIngredient(ModContent.ItemType<TimsRegret>());
        recipe.AddIngredient(ModContent.ItemType<ChromaticGauntlet>());
        recipe.AddIngredient(thoriumMod.Find<ModItem>("TheRing"));
        recipe.AddIngredient(ModContent.ItemType<NebulaEmblem>());
        recipe.AddIngredient(ModContent.ItemType<SolarEmblem>());
        recipe.AddIngredient(ModContent.ItemType<StardustEmblem>());
        recipe.AddIngredient(ModContent.ItemType<VortexEmblem>());
        recipe.AddIngredient(ModContent.ItemType<ShootingStarEmblem>());
        recipe.AddIngredient(ModContent.ItemType<HeavenlyEmblem>());
        recipe.AddIngredient(ModContent.ItemType<WhiteDwarfEmblem>());
        recipe.AddIngredient(ItemID.GravityGlobe);
        recipe.Register();
    }
}