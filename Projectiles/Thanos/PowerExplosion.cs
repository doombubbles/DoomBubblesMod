using DoomBubblesMod.Buffs;
using DoomBubblesMod.Items.Thanos;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.Thanos
{
    public class PowerExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power Explosion");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hide = true;
            Projectile.timeLeft = 60;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.timeLeft = Projectile.damage / 20;
                Projectile.localAI[0] = 1f;
            }

            for (var i = 0; i < 1; i++)
            {
                var dust = Main.dust[Dust.NewDust(Projectile.Center, 0, 0, 212, 0, 0, 0, InfinityGauntlet.PowerColor, 1.5f)];
                dust.velocity *= .5f;
                dust.noGravity = true;
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (target.HasBuff(ModContent.BuffType<PowerStoneDebuff>()))
            {
                if (target.buffTime[target.FindBuffIndex(ModContent.BuffType<PowerStoneDebuff>())] > 260
                    && Main.player[Projectile.owner].GetModPlayer<ThanosPlayer>().powerStoning.Contains(target.whoAmI))
                {
                    return false;
                }
            }

            return base.CanHitNPC(target);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<PowerStoneDebuff>(), 300);
            if (!Main.player[Projectile.owner].GetModPlayer<ThanosPlayer>().powerStoning.Contains(target.whoAmI))
            {
                Main.player[Projectile.owner].GetModPlayer<ThanosPlayer>().powerStoning.Add(target.whoAmI);
            }

            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = new Vector2(0, 0);

            return false;
        }
    }
}