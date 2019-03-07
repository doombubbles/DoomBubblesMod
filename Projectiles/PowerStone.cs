using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using DoomBubblesMod.Items;
using Microsoft.Xna.Framework.Graphics;

namespace DoomBubblesMod.Projectiles
{
    public class PowerStone : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power Stone");
        }

        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.hide = true;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
            projectile.friendly = true;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (!target.HasBuff(mod.BuffType("PowerStoneDebuff")) || target.immortal)
            {
                return false;
            }
            return true;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}