using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class Thirster : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thirster");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.alpha = 255;
            projectile.light = 0.5f;
            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.scale = 0.8f;
            aiType = 27;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.localAI[1] >= 15f)
            {
                return new Color(255, 255, 255, projectile.alpha);
            }
            if (projectile.localAI[1] < 8f)
            {
                return Color.Transparent;
            }
            int num7 = (int)((projectile.localAI[1] - 5f) / 10f * 255f);
            return new Color(num7, num7, num7, num7);
        }

        public override void AI()
        {
            if (projectile.localAI[1] > 7f)
            {
                int dust1 = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X * 4f + 2f, projectile.position.Y + 2f - projectile.velocity.Y * 4f), 8, 8, 231, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.25f);
                Main.dust[dust1].velocity *= -0.25f;
                Main.dust[dust1].noGravity = true;
                int dust2 = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X * 3f + 2f, projectile.position.Y + 2f - projectile.velocity.Y * 3f), 8, 8, 231, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.25f);
                Main.dust[dust2].velocity *= -0.25f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            int num9;
            for (int num689 = 4; num689 < 31; num689 = num9 + 1)
            {
                float num690 = projectile.oldVelocity.X * (35f / (float)num689);
                float num691 = projectile.oldVelocity.Y * (35f / (float)num689);
                int num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 8, 8, 231, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.8f);
                Main.dust[num692].noGravity = true;
                Dust dust = Main.dust[num692];
                dust.velocity *= 0.5f;
                num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 8, 8, 231, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.4f);
                dust = Main.dust[num692];
                dust.velocity *= 0.05f;
                Main.dust[num692].noGravity = true;
                num9 = num689;
            }
            return base.OnTileCollide(oldVelocity);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            int num9;
            for (int num689 = 4; num689 < 31; num689 = num9 + 1)
            {
                float num690 = projectile.oldVelocity.X * (35f / (float)num689);
                float num691 = projectile.oldVelocity.Y * (35f / (float)num689);
                int num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 8, 8, 231, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.8f);
                Main.dust[num692].noGravity = true;
                Dust dust = Main.dust[num692];
                dust.velocity *= 0.5f;
                num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 8, 8, 231, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.4f);
                dust = Main.dust[num692];
                dust.velocity *= 0.05f;
                Main.dust[num692].noGravity = true;
                num9 = num689;
            }
            
            Main.player[projectile.owner].GetModPlayer<LoLPlayer>().Lifesteal(damage * .1f, target);
            
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            int num9;
            for (int num689 = 4; num689 < 31; num689 = num9 + 1)
            {
                float num690 = projectile.oldVelocity.X * (35f / (float)num689);
                float num691 = projectile.oldVelocity.Y * (35f / (float)num689);
                int num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 8, 8, 231, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.8f);
                Main.dust[num692].noGravity = true;
                Dust dust = Main.dust[num692];
                dust.velocity *= 0.5f;
                num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 8, 8, 231, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.4f);
                dust = Main.dust[num692];
                dust.velocity *= 0.05f;
                Main.dust[num692].noGravity = true;
                num9 = num689;
            }
            base.OnHitPvp(target, damage, crit);
        }
    }
}