using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
	public class Echo : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Echo Blob");
        }

        public override void SetDefaults()
		{
            projectile.width = 8;
            projectile.height = 8;
            projectile.light = 0.3f;
            projectile.penetrate = 15;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.magic = true;
            projectile.timeLeft = 99999999;
		}

        public override void AI()
        {
            int dust1 = Dust.NewDust(new Vector2(projectile.position.X + projectile.velocity.X, projectile.position.Y + projectile.velocity.Y), projectile.width, projectile.height, 62, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 3f);
            Main.dust[dust1].noGravity = true;
            if (Main.rand.Next(10) == 0)
            {
                dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 62, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.4f);
            }
            int num40 = Dust.NewDust(new Vector2(projectile.position.X + projectile.velocity.X, projectile.position.Y + projectile.velocity.Y), projectile.width, projectile.height, 62, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 3f);
            Main.dust[num40].noGravity = true;

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0f, 4f, mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI); // /\
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0f, -4f, mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI); // \/
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 4f, 0f, mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI); // >
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, -4f, 0f, mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI); // <
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 2*(float)Math.Sqrt(2), 2*(float)Math.Sqrt(2), mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, -2*(float)Math.Sqrt(2), -2*(float)Math.Sqrt(2), mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 2*(float)Math.Sqrt(2), -2*(float)Math.Sqrt(2), mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, -2*(float)Math.Sqrt(2), 2*(float)Math.Sqrt(2), mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI);

            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);

            if (projectile.ai[0] <= 5)
            {
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                projectile.ai[0] += 1;
            }
            else
            {
                projectile.Kill();
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 4f, mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI, target.whoAmI); // /\
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, -4f, mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI, target.whoAmI); // \/
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 4f, 0f, mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI, target.whoAmI); // >
            Projectile.NewProjectile(target.Center.X, target.Center.Y, -4f, 0f, mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI, target.whoAmI); // <
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 2 * (float)Math.Sqrt(2), 2 * (float)Math.Sqrt(2), mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI, target.whoAmI);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, -2 * (float)Math.Sqrt(2), -2 * (float)Math.Sqrt(2), mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI, target.whoAmI);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 2 * (float)Math.Sqrt(2), -2 * (float)Math.Sqrt(2), mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI, target.whoAmI);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, -2 * (float)Math.Sqrt(2), 2 * (float)Math.Sqrt(2), mod.ProjectileType("Echo2"), projectile.damage / 3, projectile.knockBack / 3, Main.player[Main.myPlayer].whoAmI, target.whoAmI);
        }
    }
}