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
            projectile.width = 16;
            projectile.height = 16;
            projectile.hide = true;
            projectile.timeLeft = 60;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }

        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                projectile.timeLeft = projectile.damage / 20;
                projectile.localAI[0] = 1f;
            }

            for (var i = 0; i < 1; i++)
            {
                var dust = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 212, 0, 0, 0, InfinityGauntlet.power, 1.5f)];
                dust.velocity *= .5f;
                dust.noGravity = true;
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (target.HasBuff(mod.BuffType("PowerStoneDebuff")))
            {
                if (target.buffTime[target.FindBuffIndex(mod.BuffType("PowerStoneDebuff"))] > 260
                    && Main.player[projectile.owner].GetModPlayer<ThanosPlayer>().powerStoning.Contains(target.whoAmI))
                {
                    return false;
                }
            }

            return base.CanHitNPC(target);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("PowerStoneDebuff"), 300);
            if (!Main.player[projectile.owner].GetModPlayer<ThanosPlayer>().powerStoning.Contains(target.whoAmI))
            {
                Main.player[projectile.owner].GetModPlayer<ThanosPlayer>().powerStoning.Add(target.whoAmI);
            }

            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = new Vector2(0, 0);

            return false;
        }
    }
}