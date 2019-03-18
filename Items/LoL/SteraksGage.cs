using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod.Items.LoL
{
    class SteraksGage : ModItem
    {
        public override void SetDefaults()
        {

            item.value = 100000;
            item.width = 22;
            item.height = 20;
            item.rare = 8;
            item.accessory = true;


        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Sterak's Gage");
      Tooltip.SetDefault("Increased melee knockback and 10% increased melee speed\nTaking 25% of your current health activates Sterak's Fury");
    }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeSpeed += .10f;
            player.GetModPlayer<DoomBubblesPlayer>(mod).sterak = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PowerGlove, 1);
            recipe.AddIngredient(ItemID.WarriorEmblem, 1);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
