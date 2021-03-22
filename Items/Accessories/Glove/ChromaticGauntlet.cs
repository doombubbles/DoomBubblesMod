using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Glove
{
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
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 1);
            item.width = 36;
            item.height = 40;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += .15f;
            player.AllCrit(i => i + 5);
            player.endurance += .05f;
            player.armorPenetration += 5;
            player.AttackSpeed(f => f + .05f);
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.GetItem("CrimsonGauntlet"));
            recipe.AddIngredient(mod.GetItem("AzureGauntlet"));
            recipe.AddIngredient(mod.GetItem("JadeGauntlet"));
            recipe.AddIngredient(mod.GetItem("SaffronGauntlet"));
            recipe.AddIngredient(mod.GetItem("MeteorGauntlet"));
            recipe.AddIngredient(mod.GetItem("SepiaGauntlet"));
            recipe.AddIngredient(mod.GetItem("IndigoGauntlet"));
            recipe.AddIngredient(mod.GetItem("QuartzGauntlet"));
            recipe.AddIngredient(mod.GetItem("EbonyGauntlet"));
            recipe.AddIngredient(mod.GetItem("RoseGauntlet"));
            recipe.AddIngredient(mod.GetItem("AquamarineGauntlet"));
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}