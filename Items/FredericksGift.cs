using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod.Items
{
    class FredericksGift : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frederick's Gift");
            Tooltip.SetDefault("42% reduced mana usage");
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
            player.manaCost -= .42f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NaturesGift, 7);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
