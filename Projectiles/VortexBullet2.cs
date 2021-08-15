using DoomBubblesMod.Dusts;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class VortexBullet2 : LunarBullet
    {
        public override int DustType => ModContent.DustType<Vortex229>();

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.tileCollide = false;
            Projectile.alpha = 155;
        }
    }
}