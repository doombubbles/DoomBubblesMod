using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class Repeater : ModProjectile
    {
        
        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.extraUpdates = 2;
            projectile.scale = 1f;
            projectile.timeLeft = 600;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            player.AddBuff(mod.BuffType("FenixBombBuildUp"), 360);
            if (((projectile.ai[1] == 3 || projectile.ai[1] == -1) && player.GetModPlayer<DoomBubblesPlayer>().fenixBombBuildUp == 14)
                || !(projectile.ai[1] == 3 || projectile.ai[1] == -1) && player.GetModPlayer<DoomBubblesPlayer>().fenixBombBuildUp == 9)
            {
                Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Boung"));
            }
            player.GetModPlayer<DoomBubblesPlayer>().fenixBombBuildUp++;
            if (projectile.ai[1] == 3 || projectile.ai[1] == -1)
            {
                if (player.GetModPlayer<DoomBubblesPlayer>().fenixBombBuildUp > 15)
                {
                    player.GetModPlayer<DoomBubblesPlayer>().fenixBombBuildUp = 15;
                }
            } else if (player.GetModPlayer<DoomBubblesPlayer>().fenixBombBuildUp > 10)
            {
                player.GetModPlayer<DoomBubblesPlayer>().fenixBombBuildUp = 10;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            float originX = (float)(Main.projectileTexture[projectile.type].Width - projectile.width) * 0.5f + (float)projectile.width * 0.5f;
            int offsetX = 0;
            int offsetY = 0;
            SpriteEffects spriteEffects = SpriteEffects.None;
            Color color25 = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
            
            Rectangle value11 = new Rectangle((int)Main.screenPosition.X - 500, (int)Main.screenPosition.Y - 500, Main.screenWidth + 1000, Main.screenHeight + 1000);
            if (projectile.getRect().Intersects(value11))
            {
                Vector2 value12 = new Vector2(projectile.position.X - Main.screenPosition.X + originX + (float)offsetX, projectile.position.Y - Main.screenPosition.Y + (float)(projectile.height / 2) + projectile.gfxOffY);
                float num152 = 100f;
                float scaleFactor = 3f;
                for (int num153 = 1; num153 <= (int)projectile.localAI[0]; num153++)
                {
                    Vector2 value13 = Vector2.Normalize(projectile.velocity) * num153 * scaleFactor;
                    Color alpha2 = projectile.GetAlpha(lightColor);
                    alpha2 *= (num152 - (float)num153) / num152;
                    alpha2.A = 0;
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], value12 - value13, null, alpha2, projectile.rotation, new Vector2(originX, projectile.height / 2 + offsetY), projectile.scale, spriteEffects, 0f);
                }
            }
            return false;
        }

        public override void AI()
        {
            projectile.ai[0] = 0;
            
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.2f, 0.2f, 0.25f);
            
            float num55 = 50f;
            float num56 = 3f;
            
            projectile.localAI[0] += num56;
            if (projectile.localAI[0] > num55)
            {
                projectile.localAI[0] = num55;
            }
        }

        public override void Kill(int timeLeft)
        {
            int num293 = Main.rand.Next(3, 7);
            for (int num294 = 0; num294 < num293; num294++)
            {
                int num295 = Dust.NewDust(projectile.Center - projectile.velocity / 2f, 0, 0, 63, 0f, 0f, 100, Color.Lavender, 2.1f);
                Dust dust = Main.dust[num295];
                dust.velocity *= 2f;
                Main.dust[num295].noGravity = true;
            }
            Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Hit"));
            base.Kill(timeLeft);
        }
    }
}