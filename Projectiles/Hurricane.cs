using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace DoomBubblesMod.Projectiles
{
	public class Hurricane : ModProjectile
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hurricane Bolt");
        }
        public override void SetDefaults()
		{
            projectile.width = 6;
            projectile.height = 6;
            projectile.light = 0.3f;
            projectile.aiStyle = 1;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ranged = true;
            projectile.extraUpdates = 1;
            projectile.arrow = true;
            projectile.timeLeft = 6000;
		}

        public override void AI()
        {
            int num7;
            if (projectile.timeLeft < 5999)
            {
                for (int num137 = 0; num137 < 10; num137 = num7 + 1)
                {
                    float x2 = projectile.position.X - projectile.velocity.X / 10f * (float)num137;
                    float y2 = projectile.position.Y - projectile.velocity.Y / 10f * (float)num137;
                    int num138 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 6, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num138].alpha = projectile.alpha;
                    Main.dust[num138].position.X = x2;
                    Main.dust[num138].position.Y = y2;
                    Dust dust3 = Main.dust[num138];
                    dust3.velocity *= 0f;
                    Main.dust[num138].noGravity = true;
                    num7 = num137;
                }
            }
            float speed = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
            float oldSpeed = projectile.localAI[0];
            if (oldSpeed == 0f)
            {
                projectile.localAI[0] = speed;
                oldSpeed = speed;
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            float x = projectile.position.X;
            float y = projectile.position.Y;
            float followDistance = 300f;
            bool homing = false;
            int num144 = 0;
            if (projectile.ai[1] == 0f)
            {
                int num;
                for (int i = 0; i < 200; i = num + 1)
                {
                    if (Main.npc[i].CanBeChasedBy(projectile, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(i + 1)))
                    {
                        float targetX = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                        float targetY = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                        float distance = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - targetX) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - targetY);
                        if (distance < followDistance && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                        {
                            followDistance = distance;
                            x = targetX;
                            y = targetY;
                            homing = true;
                            num144 = i;
                        }
                    }
                    num = i;
                }
                if (homing)
                {
                    projectile.ai[1] = (float)(num144 + 1);
                }
                homing = false;
            }
            if (projectile.ai[1] > 0f)
            {
                int target = (int)(projectile.ai[1] - 1f);
                if (Main.npc[target].active && Main.npc[target].CanBeChasedBy(projectile, true) && !Main.npc[target].dontTakeDamage)
                {
                    float targetX = Main.npc[target].position.X + (float)(Main.npc[target].width / 2);
                    float targetY = Main.npc[target].position.Y + (float)(Main.npc[target].height / 2);
                    float distance = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - targetX) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - targetY);
                    if (distance < 1000f)
                    {
                        homing = true;
                        x = Main.npc[target].position.X + (float)(Main.npc[target].width / 2);
                        y = Main.npc[target].position.Y + (float)(Main.npc[target].height / 2);
                    }
                }
                else
                {
                    projectile.ai[1] = 0f;
                }
            }
            if (!projectile.friendly)
            {
                homing = false;
            }
            if (homing)
            {
                float oldSpeed2 = oldSpeed;
                Vector2 position = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float dX = x - position.X;
                float dY = y - position.Y;
                float distance = (float)Math.Sqrt((double)(dX * dX + dY * dY));
                distance = oldSpeed2 / distance;
                dX *= distance;
                dY *= distance;
                int num157 = 8;
                projectile.velocity.X = (projectile.velocity.X * (float)(num157 - 1) + dX) / (float)num157;
                projectile.velocity.Y = (projectile.velocity.Y * (float)(num157 - 1) + dY) / (float)num157;
            }
        }
    }
}