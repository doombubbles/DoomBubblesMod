using DoomBubblesMod.Content.Projectiles.Magic;

namespace DoomBubblesMod.Content.Items.Weapons;

public class ManapireKnives : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Manapire Knives");
        Tooltip.SetDefault("Rapidly throw mana stealing daggers;\n" +
                           "Or, life stealing if at max");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.VampireKnives);
        Item.DamageType = DamageClass.Magic;
        Item.shoot = ProjectileType<ManapireKnife>();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
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
        if (Main.rand.NextBool(2))
        {
            numKnives++;
        }

        if (Main.rand.NextBool(4))
        {
            numKnives++;
        }

        if (Main.rand.NextBool(8))
        {
            numKnives++;
        }

        if (Main.rand.NextBool(16))
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
        recipe.AddIngredient(ItemID.ManaCrystal, 7);
        recipe.AddTile(TileID.CrystalBall);
        recipe.Register();

        var recipe2 = CreateRecipe();
        recipe2.AddIngredient(ItemID.VampireKnives);
        recipe2.AddIngredient(ItemID.BlueDye);
        recipe2.AddTile(TileID.CrystalBall);
        recipe2.Register();
    }
}