using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.Thanos
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
            damage = 10 + (int)(target.lifeMax * .001);
            crit = false;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}