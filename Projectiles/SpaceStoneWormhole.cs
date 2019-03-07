using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace DoomBubblesMod.Projectiles
{
    public class SpaceStoneWormhole : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            projectile.width = 74;
            projectile.height = 120;
            projectile.light = 0.5f;
            projectile.friendly = true;
            projectile.scale = 1.0f;
            aiType = -1;
            Main.projFrames[projectile.type] = 4;
            projectile.timeLeft = 100;
        }

        public override void AI()
        {
            if (projectile.timeLeft % 10 == 0)
            {
                projectile.frame++;
                if (projectile.frame == 4)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}