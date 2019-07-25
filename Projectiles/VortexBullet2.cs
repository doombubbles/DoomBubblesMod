using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace DoomBubblesMod.Projectiles
{
    public class VortexBullet2 : LunarBullet
    {
        public override int DustType => mod.DustType("Vortex229");

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.tileCollide = false;
        }
    }
}