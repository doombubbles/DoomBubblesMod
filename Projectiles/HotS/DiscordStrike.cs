using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using On.Terraria;
using Terraria;
using Terraria.ModLoader;
using Dust = Terraria.Dust;
using Main = Terraria.Main;
using NPC = Terraria.NPC;

namespace DoomBubblesMod.Projectiles.HotS
{
    public class DiscordStrike : ModProjectile
    {
        private float Length => ChosenTalent == 2 || ChosenTalent == -1 ? 500f : 200f;
        private int ChosenTalent => (int) Math.Round(projectile.ai[0]);
        private float DistanceFactor => (Origin - projectile.Center).Length() / Length;
        
        public static readonly int Size = 60;

        private Vector2 Origin
        {
            get => new Vector2(projectile.localAI[0], projectile.localAI[1]);
            set {projectile.localAI[0] = value.X; projectile.localAI[1] = value.Y; }
        }
        private float Theta
        {
            get => projectile.ai[1];
            set => projectile.ai[1] = value;
        }
        
        public override void SetDefaults()
        {
            projectile.width = Size;
            projectile.height = Size;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.alpha = 69;
            projectile.timeLeft = 100;
            projectile.extraUpdates = 2;
            projectile.penetrate = -1;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (ChosenTalent == 3 || ChosenTalent == -1)
            {
                int missingHp = target.lifeMax - target.life;
                damage += (int) (damage * ((float) missingHp / target.lifeMax) + missingHp * .01);
            }
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
            if (ChosenTalent == 1 || ChosenTalent == -1)
            {
                projectile.damage = (int) (projectile.damage * 1.1);
            }
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

        public override void AI()
        {
            if (projectile.alpha == 69)
            {
                projectile.alpha = 255;
                Origin = projectile.Center;
                Theta = projectile.velocity.ToRotation();
            }

            HandlePosition();
            HandleSize();
            CreateDust();
        }

        private void HandlePosition()
        {
            projectile.position += Main.player[projectile.owner].velocity;
            Origin += Main.player[projectile.owner].velocity;
        }
        
        private void HandleSize()
        {
            Vector2 center = projectile.Center;
            projectile.width = (int) (Size * (1 - DistanceFactor));
            projectile.height = (int) (Size * (1 - DistanceFactor));
            projectile.scale = 1 - DistanceFactor;
            projectile.Center = center;

            if (projectile.width <= 0 || projectile.height <= 0)
            {
                projectile.Kill();
            }
        }

        private void CreateDust()
        {
            Vector2 line1Start = new Vector2((float) (Origin.X + Size * Math.Cos(Theta + Math.PI / 2) / 2), 
                (float) (Origin.Y + Size * Math.Sin(Theta + Math.PI / 2) / 2));
            Vector2 line2Start = new Vector2((float) (Origin.X + Size * Math.Cos(Theta - Math.PI / 2) / 2), 
                (float) (Origin.Y + Size * Math.Sin(Theta - Math.PI / 2) / 2));

            Vector2 dot = line1Start;
            float previousDistance;
            do
            {
                previousDistance = (dot - projectile.Center).Length();
                var dust = Dust.NewDustPerfect(dot, 182);
                dust.noGravity = true;
                dust.velocity = new Vector2(0, 0);
                dot.X += (float) Math.Cos((projectile.Center - dot).ToRotation()) * 2f;
                dot.Y += (float) Math.Sin((projectile.Center - dot).ToRotation()) * 2f;

            } while ((dot - projectile.Center).Length() < previousDistance);
            
            dot = line2Start;
            do
            {
                previousDistance = (dot - projectile.Center).Length();
                var dust = Dust.NewDustPerfect(dot, 182);
                dust.noGravity = true;
                dust.velocity = new Vector2(0, 0);
                dot.X += (float) Math.Cos((projectile.Center - dot).ToRotation()) * 2f;
                dot.Y += (float) Math.Sin((projectile.Center - dot).ToRotation()) * 2f;

            } while ((dot - projectile.Center).Length() < previousDistance);

        }

    }
}