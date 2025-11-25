using DoomBubblesMod.Content.Dusts;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class VortexBullet : LunarBullet
{
    protected override int DustType => DustType<Vortex229>();

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(target, hit, damageDone);
        VortexEffect(target);
    }
}