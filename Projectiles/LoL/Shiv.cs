using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class Shiv : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shiv");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.alpha = 69;
            projectile.light = 0.5f;
            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.scale = 0.8f;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.scale = 1.3f;
            aiType = 27;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 420;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (projectile.alpha == 69)
            {
                projectile.alpha = 70;
                projectile.timeLeft = player.itemAnimationMax;
                projectile.velocity = (projectile.Center - player.Center).SafeNormalize(Vector2.Zero) * 20f;

                projectile.Center -= projectile.velocity;

                projectile.ai[0] = (projectile.Center - player.Center).ToRotation();
            }

            projectile.velocity -= projectile.ai[0].ToRotationVector2() * 3f;

        }

        public override void PostAI()
        {
            projectile.rotation = projectile.ai[0] + ((float)Math.PI / 4f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            int num9;
            for (int num689 = 4; num689 < 7; num689 = num9 + 1)
            {
                float num690 = projectile.oldVelocity.X * (35f / (float)num689);
                float num691 = projectile.oldVelocity.Y * (35f / (float)num689);
                int num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 8, 8, 57, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.8f);
                Main.dust[num692].noGravity = true;
                Dust dust = Main.dust[num692];
                dust.velocity *= 0.5f;
                num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 8, 8, 57, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.4f);
                dust = Main.dust[num692];
                dust.velocity *= 0.05f;
                Main.dust[num692].noGravity = true;
                num9 = num689;
            }
            
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}