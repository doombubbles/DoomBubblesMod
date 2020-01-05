using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class IcyZone : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icy Zone");
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
            if (projectile.alpha == 69)
            {
                Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Crescent"));
                projectile.timeLeft = (int)(Main.player[projectile.owner].HeldItem.useTime * .75f);
                projectile.localAI[1] = projectile.timeLeft;

                float distance = 125 + Main.player[projectile.owner].statDefense;
                Vector2 stay = projectile.Center;
                projectile.scale = distance / 125;
                projectile.Center = stay;
                
                for (int i = 0; i <= 360; i += 4)
                {
                    Vector2 delta = ((float)Math.PI * i / 180f).ToRotationVector2();
                    Dust dust = Dust.NewDustPerfect(projectile.Center + distance * delta, 135, -delta * distance / 12f, 0, default, 2.0f);
                    dust.noGravity = true;
                    delta = ((float)Math.PI * (i + 2) / 180f).ToRotationVector2();
                    dust = Dust.NewDustPerfect(projectile.Center + distance * delta, 135, -delta * distance / 11f, 0, default, 2.0f);
                    dust.noGravity = true;
                }
            }

            projectile.alpha = 255;

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        
        
    }
}