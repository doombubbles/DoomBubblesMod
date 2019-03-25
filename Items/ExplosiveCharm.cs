using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod.Items
{
    class ExplosiveCharm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosive Charm");
            Tooltip.SetDefault("Explosive Bullets deal more damage and can no longer hurt you");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 2, 0 ,0);
            item.width = 28;
            item.height = 26;
            item.rare = 4;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>().noExplosionBulletDamage = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ExplosivePowder, 100);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
