using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod.Items.LoL
{
    class Deathcap : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rabadon's Deathcap");
            Tooltip.SetDefault("10% increased magic damage\n" +
                               "Magic damage increases are 25% more effective");
        }

        public override void SetDefaults()
        {
            item.value = 100000;
            item.width = 34;
            item.height = 22;
            item.rare = 8;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += .1f;
            player.GetModPlayer<DoomBubblesPlayer>().magicMult += .25f;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WizardHat, 1);
            recipe.AddIngredient(ItemID.SorcererEmblem, 1);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.WizardsHat, 1);
            recipe2.AddIngredient(ItemID.SorcererEmblem, 1);
            recipe2.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe2.AddTile(TileID.CrystalBall);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}
