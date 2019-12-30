using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class Cleaver : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cleaver");
        }

        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.alpha = 100;
            projectile.light = 0.5f;
            projectile.aiStyle = 18;
            projectile.friendly = true;
            projectile.penetrate = 7;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.timeLeft = 180;
            projectile.scale = 1.3f;
            aiType = 274;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.timeLeft < 85)
            {
                byte b2 = (byte)(projectile.timeLeft * 3);
                byte a2 = (byte)(100f * ((float)b2 / 255f));
                return new Color((int)b2, (int)b2, (int)b2, (int)a2);
            }
            return new Color(255, 255, 255, 127);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (Main.rand.Next(5) == 0)
            {
                if (projectile.timeLeft < 85)
                {
                    byte b2 = (byte)(projectile.timeLeft * 3);
                    byte a2 = (byte)(100f * ((float)b2 / 255f));
                    int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 60, 0, 0, 255 - a2);
                }
                else
                {
                    int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 60, 0, 0, 0);
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.ai[1] < 1)
            {
                target.AddBuff(mod.BuffType("Cleaved"), 300);
                projectile.ai[1] = 2;
            }
            
            Main.player[projectile.owner].AddBuff(mod.BuffType("Rage"), target.life <= 0 ? 180 : 60);
        }
    }
}