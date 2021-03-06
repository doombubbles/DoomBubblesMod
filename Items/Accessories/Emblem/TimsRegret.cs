﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Emblem
{
    class TimsRegret : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tim's Regret");
            Tooltip.SetDefault("25% increased damage\n100% increased n00b regret");
        }

        public override void SetDefaults()
        {
            item.value = 100000;
            item.width = 28;
            item.height = 28;
            item.rare = 9;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += .25f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WarriorEmblem);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddIngredient(ItemID.RangerEmblem);
            recipe.AddIngredient(ItemID.SummonerEmblem);
            recipe.AddIngredient(ItemID.AvengerEmblem);
            recipe.AddIngredient(ItemID.DestroyerEmblem);
            recipe.AddIngredient(ItemID.CelestialEmblem);


            if (DoomBubblesMod.thoriumLoaded.HasValue && DoomBubblesMod.thoriumLoaded.Value)
            {
                addThoriumRecipes(ref recipe);
            }

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public void addThoriumRecipes(ref ModRecipe recipe)
        {
            Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
            recipe.AddIngredient(thoriumMod.ItemType("BardEmblem"));
            recipe.AddIngredient(thoriumMod.ItemType("NinjaEmblem"));
            recipe.AddIngredient(thoriumMod.ItemType("ClericEmblem"));
            recipe.AddIngredient(thoriumMod.ItemType("ArcaniteEmblem"));
        }
    }
}
