using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod.Items.LoL
{
    class CloakOfAgility : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cloak of Agility");
            Tooltip.SetDefault("Your Crit Chance is 1.5x higher\n" +
                               "Causes stars to fall when damaged");
        }

        public override void SetDefaults()
        {
            item.value = 100000;
            item.width = 34;
            item.height = 22;
            item.rare = 9;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.starCloak = true;
            /*
            player.magicCrit *= 2;
            player.meleeCrit *= 2;
            player.rangedCrit *= 2;
            player.thrownCrit *= 2;
            */
            player.GetModPlayer<DoomBubblesPlayer>().critChanceMult *= 1.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StarCloak);
            for (int i = 0; i < 3; i++)
            {
                recipe.AddIngredient(ItemID.EyeoftheGolem);
            }
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
