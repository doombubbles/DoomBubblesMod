using DoomBubblesMod.Content.Dusts;
using Terraria;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class SolarBullet : LunarBullet
{
    protected override int DustType => DustType<Solar229>();

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        SolarEffect();
    }
}