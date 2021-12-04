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
        player.endurance += .05f;
        player.armorPenetration += 5;
        player.AttackSpeed(f => f + .05f);
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<CrimsonGauntlet>());
        recipe.AddIngredient(ModContent.ItemType<AzureGauntlet>());
        recipe.AddIngredient(ModContent.ItemType<JadeGauntlet>());
        recipe.AddIngredient(ModContent.ItemType<SaffronGauntlet>());
        recipe.AddIngredient(ModContent.ItemType<MeteorGauntlet>());
        recipe.AddIngredient(ModContent.ItemType<SepiaGauntlet>());
        recipe.AddIngredient(ModContent.ItemType<IndigoGauntlet>());
        recipe.AddIngredient(ModContent.ItemType<QuartzGauntlet>());
        recipe.AddIngredient(ModContent.ItemType<EbonyGauntlet>());
        recipe.AddIngredient(ModContent.ItemType<RoseGauntlet>());
        recipe.AddIngredient(ModContent.ItemType<AquamarineGauntlet>());
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}