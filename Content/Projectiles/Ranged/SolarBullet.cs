using DoomBubblesMod.Content.Dusts;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class SolarBullet : LunarBullet
{
    protected override int DustType => DustType<Solar229>();

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        SolarEffect();
    }
}