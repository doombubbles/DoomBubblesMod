using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Glove
{
    class ChromaticGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chromatic Gauntlet");
            Tooltip.SetDefault("7% increased damage\n" +
                               "7% increased crit chance\n" +
                               "7% increased attack speed\n" +
                               "7% reduced damage taken\n" +
                               "Increases armor penetration by 7");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 1, 0 ,0);
            item.width = 36;
            item.height = 40;
            item.rare = 3;
            item.accessory = true;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += .07f;
            player.meleeCrit += 7;
            player.rangedCrit += 7;
            player.magicCrit += 7;
            player.thrownCrit += 7;
            player.GetModPlayer<DoomBubblesPlayer>().customRadiantCrit += 7;
            player.GetModPlayer<DoomBubblesPlayer>().customSymphonicCrit += 7;
            player.endurance += .07f;
            player.armorPenetration += 7;
            if (ModLoader.GetMod("GottaGoFast") != null)
            {
                ModLoader.GetMod("GottaGoFast").Call("attackSpeed", player.whoAmI, .07f);
            }
            else
            {
                player.meleeSpeed += .07f;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
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
