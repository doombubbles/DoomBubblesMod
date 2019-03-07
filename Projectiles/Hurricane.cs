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
            float num139 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
            float num140 = projectile.localAI[0];
            if (num140 == 0f)
            {
                projectile.localAI[0] = num139;
                num140 = num139;
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            float num141 = projectile.position.X;
            float num142 = projectile.position.Y;
            float num143 = 300f;
            bool flag4 = false;
            int num144 = 0;
            if (projectile.ai[1] == 0f)
            {
                int num;
                for (int num145 = 0; num145 < 200; num145 = num + 1)
                {
                    if (Main.npc[num145].CanBeChasedBy(projectile, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num145 + 1)))
                    {
                        float num146 = Main.npc[num145].position.X + (float)(Main.npc[num145].width / 2);
                        float num147 = Main.npc[num145].position.Y + (float)(Main.npc[num145].height / 2);
                        float num148 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num146) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num147);
                        if (num148 < num143 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num145].position, Main.npc[num145].width, Main.npc[num145].height))
                        {
                            num143 = num148;
                            num141 = num146;
                            num142 = num147;
                            flag4 = true;
                            num144 = num145;
                        }
                    }
                    num = num145;
                }
                if (flag4)
                {
                    projectile.ai[1] = (float)(num144 + 1);
                }
                flag4 = false;
            }
            if (projectile.ai[1] > 0f)
            {
                int num149 = (int)(projectile.ai[1] - 1f);
                if (Main.npc[num149].active && Main.npc[num149].CanBeChasedBy(projectile, true) && !Main.npc[num149].dontTakeDamage)
                {
                    float num150 = Main.npc[num149].position.X + (float)(Main.npc[num149].width / 2);
                    float num151 = Main.npc[num149].position.Y + (float)(Main.npc[num149].height / 2);
                    float num152 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num150) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num151);
                    if (num152 < 1000f)
                    {
                        flag4 = true;
                        num141 = Main.npc[num149].position.X + (float)(Main.npc[num149].width / 2);
                        num142 = Main.npc[num149].position.Y + (float)(Main.npc[num149].height / 2);
                    }
                }
                else
                {
                    projectile.ai[1] = 0f;
                }
            }
            if (!projectile.friendly)
            {
                flag4 = false;
            }
            if (flag4)
            {
                float num153 = num140;
                Vector2 vector13 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num154 = num141 - vector13.X;
                float num155 = num142 - vector13.Y;
                float num156 = (float)Math.Sqrt((double)(num154 * num154 + num155 * num155));
                num156 = num153 / num156;
                num154 *= num156;
                num155 *= num156;
                int num157 = 8;
                projectile.velocity.X = (projectile.velocity.X * (float)(num157 - 1) + num154) / (float)num157;
                projectile.velocity.Y = (projectile.velocity.Y * (float)(num157 - 1) + num155) / (float)num157;
            }
        }
    }
}