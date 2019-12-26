using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class ArcaneComet : HappyProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arcane Comet");
            Main.projFrames[projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            projectile.width = 62;
            projectile.height = 62;
            projectile.scale = .5f;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 1000;
            projectile.penetrate = -1; 
            projectile.alpha = 69;
            projectile.usesLocalNPCImmunity = true;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (1000 - projectile.timeLeft <= projectile.ai[0])
            {
                return false;
            }

            if (1000 - projectile.timeLeft > (int) projectile.ai[0] + 1)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }

        public override void AI()
        {
            if (1000 - projectile.timeLeft > projectile.ai[0])
            {
                projectile.frame++;
                projectile.rotation = 0f;
                if (projectile.velocity != new Vector2(0, 0))
                {
                    var center = projectile.Center;
                    projectile.scale = 1.5f;
                    projectile.Center = center;
                }

                
                projectile.velocity = new Vector2(0,0);
                
                if (projectile.frame == 8)
                {
                    projectile.Kill();
                }
            }
            else
            {
                projectile.rotation += .05f;
            }
        }
    }
}