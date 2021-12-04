using DoomBubblesMod.Content.Dusts;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

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