using DoomBubblesMod.Dusts;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class NebulaBullet : LunarBullet
    {
        public override int DustType => ModContent.DustType<Nebula229>();

        public override void AI()
        {
            base.AI();
            NebulaEffect();
        }
    }
}