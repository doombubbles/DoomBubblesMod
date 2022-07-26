using System;
using DoomBubblesMod.Content.Dusts;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class StardustBullet2 : LunarBullet
{
    protected override int DustType => DustType<Stardust229>();

    public override bool? CanHitNPC(NPC target)
    {
        return (int) Math.Round(Projectile.ai[1]) == target.whoAmI ? false : null;
    }
}