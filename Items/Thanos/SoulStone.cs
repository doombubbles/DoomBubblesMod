using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Thanos
{
    internal class SoulStone : ModItem
    {
        public override void SetDefaults()
        {
            item.value = Item.sellPrice(5);
            ;
            item.rare = -11;
            item.height = 20;
            item.width = 14;
            item.useStyle = 4;
        }

        public override bool UseItem(Player player)
        {
            player.KillMe(
                PlayerDeathReason.ByCustomReason(player.name + " wielded power beyond " +
                                                 (player.Male ? "his" : "her") + " control."), 0, 0);

            for (var i = 0; i <= 360; i += 5)
            {
                var rad = Math.PI * i / 180;
                var dX = (float) (10 * Math.Cos(rad));
                var dY = (float) (10 * Math.Sin(rad));
                var dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY),
                    0, InfinityGauntlet.soul, 1.5f);
                dust.noGravity = true;
            }

            return base.UseItem(player);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Stone");
            Tooltip.SetDefault("\"Soul holds a special place among the Infinity Stones.\n" +
                               "You might say, it is a certain wisdom. To ensure that\n" +
                               "whoever possesses it understands its power, the stone\n" +
                               "demands a sacrifice. In order to take the stone, you\n" +
                               "must lose that which you love. A soul for a soul.\"\n" +
                               "-The Stonekeeper");
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Amber);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}