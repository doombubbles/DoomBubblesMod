using System;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class RangedDamage : Damage
    {
        public override void SetDefaults()
        {
            projectile.ranged = true;
        }
    }
}