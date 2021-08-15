using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.Thanos
{
    public class SpaceStoneWormhole : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 74;
            Projectile.height = 120;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.scale = 1.0f;
            AIType = -1;
            Main.projFrames[Projectile.type] = 4;
            Projectile.timeLeft = 100;
        }

        public override void AI()
        {
            if (Projectile.timeLeft % 10 == 0)
            {
                Projectile.frame++;
                if (Projectile.frame == 4)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}