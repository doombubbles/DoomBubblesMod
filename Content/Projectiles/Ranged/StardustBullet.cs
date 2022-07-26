using DoomBubblesMod.Content.Dusts;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class StardustBullet : LunarBullet
{
    protected override int DustType => DustType<Stardust229>();

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        base.OnHitNPC(target, damage, knockback, crit);
        StardustEffect(target);
    }
}