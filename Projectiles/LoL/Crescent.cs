using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class Crescent : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crescent");
        }

        public override void SetDefaults()
        {
            projectile.width = 250;
            projectile.height = 250;
            projectile.light = 0.5f;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.timeLeft = 420;
            projectile.alpha = 69;
            //Main.projFrames[projectile.type] = 5;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 420;
        }

        public override void AI()
        {
            var player = Main.player[projectile.owner];
            if (projectile.ai[0] == 0)
            {
                projectile.Center = new Vector2(player.Center.X, player.Center.Y);
            }

            Vector2 pos = projectile.ai[0] == 1 ? projectile.Center : player.Center;
            
            if (projectile.alpha == 69)
            {
                Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Crescent"));
                projectile.timeLeft = (int)(Main.player[projectile.owner].HeldItem.useTime * .75f);
                projectile.localAI[1] = projectile.timeLeft;
                
                
                for (int i = 0; i <= 360; i += 4)
                {
                    double rad = (Math.PI * i) / 180;
                    float dX = (float) (12 * Math.Cos(rad));
                    float dY = (float) (12 * Math.Sin(rad));
                    Dust dust = Dust.NewDustPerfect(new Vector2(pos.X, pos.Y), 6, new Vector2(dX, dY), 0, new Color(255, 50, 0), 2.0f);
                    dust.noGravity = true;
                    if (projectile.ai[0] == 0)
                    {
                        dust.velocity += player.velocity;
                    }
                }
                
                for (int i = 2; i <= 360; i += 4)
                {
                    double rad = (Math.PI * i) / 180;
                    float dX = (float) (11.5 * Math.Cos(rad));
                    float dY = (float) (11.5 * Math.Sin(rad));
                    Dust dust = Dust.NewDustPerfect(new Vector2(pos.X, pos.Y), 6, new Vector2(dX, dY), 0, new Color(255, 50, 0), 2.0f);
                    dust.noGravity = true;
                    if (projectile.ai[0] == 0)
                    {
                        dust.velocity += player.velocity;
                    }
                }
            }

            projectile.alpha = 255;

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] > .1f)
            {
                Main.player[projectile.owner].GetModPlayer<LoLPlayer>().Lifesteal(damage * .12f, target, true);
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        
        
    }
}