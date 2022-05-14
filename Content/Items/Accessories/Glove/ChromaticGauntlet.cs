using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Glove;

internal class ChromaticGauntlet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Chromatic Gauntlet");
        Tooltip.SetDefault("5% increased damage\n" +
                           "5% increased crit chance\n" +
                           "5% increased attack speed\n" +
                           "5% reduced damage taken\n" +
                           "Increases armor penetration by 5");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(0, 1);
        Item.width = 36;
        Item.height = 40;
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += .05f;
        player.GetCritChance(DamageClass.Generic) += 5;
        player.GetArmorPenetration(DamageClass.Generic) += 5;
        player.GetAttackSpeed(DamageClass.Generic) += .05f;
        player.endurance += .05f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemType<CrimsonGauntlet>());
        recipe.AddIngredient(ItemType<AzureGauntlet>());
        recipe.AddIngredient(ItemType<JadeGauntlet>());
        recipe.AddIngredient(ItemType<SaffronGauntlet>());
        recipe.AddIngredient(ItemType<MeteorGauntlet>());
        recipe.AddIngredient(ItemType<SepiaGauntlet>());
        recipe.AddIngredient(ItemType<IndigoGauntlet>());
        recipe.AddIngredient(ItemType<QuartzGauntlet>());
        recipe.AddIngredient(ItemType<EbonyGauntlet>());
        recipe.AddIngredient(ItemType<RoseGauntlet>());
        recipe.AddIngredient(ItemType<AquamarineGauntlet>());
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}