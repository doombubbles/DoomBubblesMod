using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Weapons
{
    public class TrueMidnightMaelstrom : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("This is a modded gun.");
            DisplayName.SetDefault("True Midnight Maelstrom");
        }

        public override void SetDefaults()
        {
            item.damage = 60;
            item.ranged = true;
            item.width = 58;
            item.height = 24;
            item.useTime = 27;
            item.useAnimation = 27;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 10);
            item.rare = 8;
            item.UseSound = SoundID.Item41;
            item.autoReuse = false;
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 10f;
            item.useAmmo = AmmoID.Bullet;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            // Here we use the multiplicative damage modifier because Terraria does this approach for Ammo damage bonuses. 
            mult *= player.bulletDamage;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("MidnightMaelstrom"));
            recipe.AddIngredient(mod.ItemType("BrokenHeroGun"));
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item36, position);
            var numberProjectiles = 3 + Main.rand.Next(2);
            for (var i = 0; i < numberProjectiles; i++)
            {
                var perturbedSpeed =
                    new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
                // If you want to randomize the speed to stagger the projectiles
                var scale = 1f - Main.rand.NextFloat() * .1f;
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type,
                    (int) (damage / 2.0), knockBack, player.whoAmI);
            }

            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("MidnightBlast"),
                damage, knockBack, player.whoAmI);


            return false;
        }


        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
    }
}