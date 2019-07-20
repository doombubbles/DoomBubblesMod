using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.HotS
{
    public class PhaseBomb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phase Bomb");
            Main.projFrames[projectile.type] = 9;
        }

        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.scale = .33f;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 1000;
            projectile.ranged = true;
            projectile.penetrate = -1; 
            projectile.alpha = 255;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D = Main.projectileTexture[projectile.type];
            int height = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y = height * projectile.frame;
            
            
            Vector2 pos = (projectile.position + new Vector2(projectile.width, projectile.height) / 2f +
                                Vector2.UnitY * projectile.gfxOffY - Main.screenPosition).Floor();
            
            spriteBatch.Draw(Main.projectileTexture[projectile.type], pos, new Rectangle(0, y, texture2D.Width, height), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture2D.Width / 2f, (float) height / 2f), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (1000 - projectile.timeLeft <= projectile.ai[0])
            {
                return projectile.ai[1] == 1 || projectile.ai[1] == -1;
            }
            return 1000 - projectile.timeLeft == (int)projectile.ai[0] + 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.localAI[0]++;
        }

        public override void AI()
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 15;
            }
            else
            {
                projectile.alpha = 0;
            }
            
            
            Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.4f, 0.4f, 0.5f);

            var center = projectile.Center;
            
            if (1000 - projectile.timeLeft <= projectile.ai[0])
            {
                projectile.frame = (projectile.timeLeft / 10) % 2;
                projectile.velocity *= 1.015f;
            }
            else
            {
                projectile.width = (int) (20 * projectile.knockBack);
                projectile.height = (int) (20 * projectile.knockBack);
                projectile.scale = projectile.knockBack / 5f;
                projectile.Center = center;
                if (projectile.velocity != new Vector2(0, 0))
                {
                    int npcs = 0;
                    for (var i = 0; i < Main.npc.Length; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (!npc.active || npc.friendly)
                        {
                            continue;
                        }
                        if (projectile.Hitbox.Intersects(npc.getRect()))
                        {
                            npcs++;
                            npc.immune[projectile.owner] = 0;
                        }
                    }

                    if (npcs == 1 && (projectile.ai[1] == 2 || projectile.ai[1] == -1))
                    {
                        projectile.damage = (int) (projectile.damage * 2 * projectile.knockBack / 5f);
                    }
                    
                    Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Bomb"), projectile.knockBack / 5f);
                }
                
                projectile.velocity = new Vector2(0,0);

                projectile.frame++;
                

                if (projectile.frame == 9)
                {
                    projectile.Kill();
                }

            }



        }

        public override void Kill(int timeLeft)
        {
            if (projectile.localAI[0] > 0)
            {
                Player player = Main.player[projectile.owner];
                player.AddBuff(mod.BuffType("FenixRepeaterBuff"), 360);
                player.GetModPlayer<HotSPlayer>().fenixRepeaterBuff += (int) projectile.localAI[0];
                if (player.GetModPlayer<HotSPlayer>().fenixRepeaterBuff > 10)
                {
                    player.GetModPlayer<HotSPlayer>().fenixRepeaterBuff = 10;
                }
            }
            base.Kill(timeLeft);
        }
    }
}