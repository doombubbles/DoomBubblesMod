using DoomBubblesMod.Content.Dusts;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class NebulaBullet : LunarBullet
{
    public override int DustType => ModContent.DustType<Nebula229>();

    public override void AI()
    {
        base.AI();
        NebulaEffect();
    }
}