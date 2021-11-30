using System;
using DoomBubblesMod.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Weapons
{
    public class BloodburstBlunderbuss : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("This is a modded gun.");
            DisplayName.SetDefault("Bloodburst Blunderbuss");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.damage = 28;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 52;
            Item.height = 20;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = 5;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 4;
            Item.value = 54000;
            Item.rare = 3;
            Item.UseSound = SoundID.Item41;
            Item.autoReuse = false;
            Item.shoot = 10; //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TheUndertaker);
            recipe.AddIngredient(ItemID.Boomstick);
            recipe.AddIngredient(ItemID.PhoenixBlaster);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();

            var recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<MidnightMaelstrom>());
            recipe2.AddTile(TileID.DemonAltar);
            recipe2.Register();
        }

        // What if I wanted this gun to have a 38% chance not to consume ammo?
        /*public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() >= .38f;
        }*/

        // What if I wanted it to work like Uzi, replacing regular bullets with High Velocity Bullets?
        // Uzi/Molten Fury style: Replace normal Bullets with Highvelocity
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.Bullet) // or ProjectileID.WoodenArrowFriendly
            {
                type = ProjectileID.BulletHighVelocity; // or ProjectileID.FireArrow;
            }
            return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
        }*/

        // What if I wanted it to shoot like a shotgun?
        // Shotgun style: Multiple Projectiles, Random spread 
        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type,
            int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item36, position);

            var number = 3 + Main.rand.Next(2);

            var d = (Main.MouseWorld - player.Center).ToRotation();

            for (var i = 1; i <= number; i++)
            {
                var pos = position + ((float) (d + Math.PI / 2)).ToRotationVector2() * 10 +
                          ((float) (d - Math.PI / 2)).ToRotationVector2() * (20f / i);
                Projectile.NewProjectile(source, pos, velocity, type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        // What if I wanted an inaccurate gun? (Chain Gun)
        // Inaccurate Gun style: Single Projectile, Random spread 
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }*/

        // What if I wanted multiple projectiles in a even spread? (Vampire Knives) 
        // Even Arc style: Multiple Projectile, Even Spread 
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 3 + Main.rand.Next(3); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(45);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 Projectile.
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }*/

        // Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }

        // How can I make the shots appear out of the muzzle exactly?
        // Also, when I do this, how do I prevent shooting through tiles?
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }*/

        // How can I get a "Clockwork Assault Riffle" effect?
        // 3 round burst, only consume 1 ammo for burst. Delay between bursts, use reuseDelay
        /*	The following changes to SetDefaults()
             Item.useAnimation = 12;
            Item.useTime = 4;
            Item.reuseDelay = 14;
        public override bool ConsumeAmmo(Player player)
        {
            // Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
            // We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
            return !(player.itemAnimation < Item.useAnimation - 2);
        }*/

        // How can I shoot 2 different projectiles at the same time?
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // Here we manually spawn the 2nd projectile, manually specifying the projectile type that we wish to shoot.
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.GrenadeI, damage, knockBack, player.whoAmI);
            // By returning true, the vanilla behavior will take place, which will shoot the 1st projectile, the one determined by the ammo.
            return true;
        }*/
    }
}