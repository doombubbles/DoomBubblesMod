using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.HotS
{
    public class PylonLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pylon Laser");
            ProjectileID.Sets.SentryShot[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 200;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
        }


        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(new Vector2((float) (projectile.position.X), (float) (projectile.position.Y)), 180);
            dust.noGravity = true;
        }
    }
}