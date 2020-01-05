using System;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class MagicDamage : Damage
    {
        public override void SetDefaults()
        {
            projectile.magic = true;
        }
    }
}