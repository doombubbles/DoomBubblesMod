using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class TriPhage : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.melee = true;
            projectile.light = 0.5f;
            projectile.alpha = 255;
            projectile.friendly = true;
        }
        
        public override string Texture => "DoomBubblesMod/Items/LoL/Advanced/Phage";

        public override void AI()
        {
            if (projectile.localAI[1] < 15f)
            {
                ref float reference = ref projectile.localAI[1];
                reference += 1f;
            }
            else
            {
                if (projectile.localAI[0] == 0f)
                {
                    projectile.scale -= 0.02f;
                    projectile.alpha += 30;
                    if (projectile.alpha >= 250)
                    {
                        projectile.alpha = 255;
                        projectile.localAI[0] = 1f;
                    }
                }
                else if (projectile.localAI[0] == 1f)
                {
                    projectile.scale += 0.02f;
                    projectile.alpha -= 30;
                    if (projectile.alpha <= 0)
                    {
                        projectile.alpha = 0;
                        projectile.localAI[0] = 0f;
                    }
                }
            }
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(SoundID.Item8, projectile.position);
            }
            projectile.rotation += (float)projectile.direction * 0.4f;
            projectile.spriteDirection = projectile.direction;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            Main.player[projectile.owner].AddBuff(mod.BuffType("Rage"), target.life <= 0 ? 240 : 120);
        }

        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
            int num534;
            for (int num802 = 4; num802 < 16; num802 = num534 + 1)
            {
                int num803 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 194, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100);
                Main.dust[num803].noGravity = true;
                Dust dust = Main.dust[num803];
                dust.velocity *= 0.5f;
                num534 = num802;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.localAI[1] >= 15f)
            {
                return new Color(255, 255, 255, projectile.alpha);
            }
            if (projectile.localAI[1] < 5f)
            {
                return Color.Transparent;
            }
            int num7 = (int)((projectile.localAI[1] - 5f) / 10f * 255f);
            return new Color(num7, num7, num7, num7);
        }
    }
}