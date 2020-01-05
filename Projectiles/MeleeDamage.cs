using System;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class MeleeDamage : Damage
    {
        public override void SetDefaults()
        {
            projectile.melee = true;
        }
    }
}