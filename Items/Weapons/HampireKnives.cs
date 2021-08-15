using DoomBubblesMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Weapons
{
    public class HampireKnives : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hampire Knives");
            Tooltip.SetDefault("Rapidly throw food stealing daggers;\n" +
                               "Or, life stealing if at max duration");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.VampireKnives);
            //Item.DamageType = DamageClass.Melee;
            Item.DamageType = DamageClass.Ranged;
            Item.shoot = ModContent.ProjectileType<HampireKnife>();
        }


        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type,
            int damage, float knockback)
        {
            /*
            Vector2 position2 = player.RotatedRelativePoint(player.MountedCenter);
            
            if (player.whoAmI == Main.myPlayer)
            {
                float speed = Item.shootSpeed;
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
                var perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(35)); // 30 degree spread.
                // If you want to randomize the speed to stagger the projectiles
                // float scale = 1f - (Main.rand.NextFloat() * .3f);
                // perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage,
                    knockback, player.whoAmI);
            }

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.VampireKnives);
            recipe.AddIngredient(ItemID.Bacon, 7);
            recipe.AddTile(TileID.MeatGrinder);
            recipe.Register();
        }
    }
}