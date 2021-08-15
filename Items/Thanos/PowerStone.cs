using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Thanos
{
    internal class PowerStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power Stone");
            Tooltip.SetDefault("\"The stone reacts to anything organic, the bigger\n" +
                               "the target, the bigger the power surge.\"\n" +
                               "-Gamora");
            
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(5);
            Item.rare = ItemRarityID.Purple;
            Item.height = 20;
            Item.width = 14;
            Item.useStyle = 4;
        }

        public override bool? UseItem(Player player)
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
                    0, InfinityGauntlet.power, 1.5f);
                dust.noGravity = true;
            }

            return base.UseItem(player);
        }
        
        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Amethyst);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}