using DoomBubblesMod.Content.Dusts;
using Terraria;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class StardustBullet : LunarBullet
{
    protected override int DustType => DustType<Stardust229>();

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(target, hit, damageDone);
        StardustEffect(target);
    }
}