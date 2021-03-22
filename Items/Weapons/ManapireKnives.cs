using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Weapons
{
    public class ManapireKnives : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Manapire Knives");
            Tooltip.SetDefault("Rapidly throw mana stealing daggers;\n" +
                               "Or, life stealing if at max");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.VampireKnives);
            item.melee = false;
            item.magic = true;
            item.shoot = mod.ProjectileType("ManapireKnife");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage,
            ref float knockBack)
        {
            /*
            Vector2 position2 = player.RotatedRelativePoint(player.MountedCenter);
            
            if (player.whoAmI == Main.myPlayer)
            {
                float speed = item.shootSpeed;
                float dX = Main.mouseX - Main.screenWidth / 2;
                float dY = Main.mouseY - Main.screenHeight / 2;
                float distance = (float)Math.Sqrt(dX * dX + dY * dY);
                */
            var numKnives = 4;
            if (Main.rand.Next(2) == 0)
            {
                numKnives++;
            }

            if (Main.rand.Next(4) == 0)
            {
                numKnives++;
            }

            if (Main.rand.Next(8) == 0)
            {
                numKnives++;
            }

            if (Main.rand.Next(16) == 0)
            {
                numKnives++;
            }
            /*
                for (int i = 0; i < numKnives; i++)
                {
                    float num140 = dX;
                    float num141 = dY;
                    float num142 = (float)i;
                    num140 += (float)Main.rand.Next(-35, 36) * num142;
                    num141 += (float)Main.rand.Next(-35, 36) * num142;
                    distance = (float)Math.Sqrt(num140 * num140 + num141 * num141);
                    distance = speed / distance;
                    num140 *= distance;
                    num141 *= distance;
                    float x5 = position2.X;
                    float y5 = position2.Y;
                    Projectile.NewProjectile(x5, y5, num140, num141, type, damage, knockBack, player.whoAmI);
                }
                
            }
            */

            for (var i = 0; i < numKnives; i++)
            {
                var perturbedSpeed =
                    new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(35)); // 30 degree spread.
                // If you want to randomize the speed to stagger the projectiles
                // float scale = 1f - (Main.rand.NextFloat() * .3f);
                // perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage,
                    knockBack, player.whoAmI);
            }

            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VampireKnives);
            recipe.AddIngredient(ItemID.ManaCrystal, 7);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();

            var recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.VampireKnives);
            recipe2.AddIngredient(ItemID.BlueDye);
            recipe2.AddTile(TileID.CrystalBall);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}