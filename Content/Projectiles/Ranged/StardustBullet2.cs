using System;
using DoomBubblesMod.Content.Dusts;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class StardustBullet2 : LunarBullet
{
    public override int DustType => DustType<Stardust229>();

    public override bool? CanHitNPC(NPC target)
    {
        if ((int) Math.Round(Projectile.ai[1]) == target.whoAmI)
        {
            return false;
        }

        return base.CanHitNPC(target);
    }
}