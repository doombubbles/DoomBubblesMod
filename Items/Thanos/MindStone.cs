using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Thanos
{
    internal class MindStone : ModItem
    {
        public override void SetDefaults()
        {
            item.value = Item.sellPrice(5);
            ;
            item.rare = 8;
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
                    0, InfinityGauntlet.mind, 1.5f);
                dust.noGravity = true;
            }

            return base.UseItem(player);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mind Stone");
            Tooltip.SetDefault("\"The Mind Stone is the fourth of the Infinity Stones to show up in the last\n" +
                               "few years. It's not a coincidence. Someone has been playing an intricate\n" +
                               "game and has made pawns of us.\"\n" +
                               "-Thor");
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Topaz);
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