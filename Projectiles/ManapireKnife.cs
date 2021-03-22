using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class ManapireKnife : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Manapire Knife");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.VampireKnife);
            projectile.melee = false;
            projectile.magic = true;
            projectile.aiStyle = -1;
        }

        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                ref var referenc = ref projectile.localAI[0];
                referenc += 1f;
                projectile.alpha = 0;
            }

            projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.03f *
                                   projectile.direction;

            ref var reference = ref projectile.ai[0];
            reference += 1f;
            if (projectile.ai[0] >= 30f)
            {
                projectile.alpha += 10;
                projectile.damage = (int) (projectile.damage * 0.9);
                projectile.knockBack = (int) (projectile.knockBack * 0.9);
                if (projectile.alpha >= 255)
                {
                    projectile.active = false;
                }
            }

            if (projectile.ai[0] < 30f)
            {
                projectile.rotation = (float) Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            var player = Main.player[projectile.owner];

            if ((player.statLife < player.statLifeMax2 && !player.moonLeech && target.FullName != "Target Dummy"
                 || player.statMana < player.statManaMax2) && target.lifeMax > 5 && damage > 0)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f,
                    mod.ProjectileType("ManapireRestore"), 0, 0f, projectile.owner, projectile.owner, damage);
            }
        }

        public override void Kill(int timeLeft)
        {
            for (var num401 = 0; num401 < 3; num401++)
            {
                var num402 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, mod.DustType("ManapireDust"), 0f, 0f, 100, default, 0.8f);
                Main.dust[num402].noGravity = true;
                var dust = Main.dust[num402];
                dust.velocity *= 1.2f;
                dust = Main.dust[num402];
                dust.velocity -= projectile.oldVelocity * 0.3f;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha,
                (byte) ((255 - projectile.alpha) / 3f));
        }
    }
}