namespace DoomBubblesMod.Projectiles
{
    public class VortexBullet2 : LunarBullet
    {
        public override int DustType => mod.DustType("Vortex229");

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.tileCollide = false;
            projectile.alpha = 155;
        }
    }
}