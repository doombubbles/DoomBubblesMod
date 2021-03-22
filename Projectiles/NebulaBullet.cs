namespace DoomBubblesMod.Projectiles
{
    public class NebulaBullet : LunarBullet
    {
        public override int DustType => mod.DustType("Nebula229");

        public override void AI()
        {
            base.AI();
            NebulaEffect();
        }
    }
}