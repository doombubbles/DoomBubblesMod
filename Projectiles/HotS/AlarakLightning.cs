using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.HotS
{
    public class AlarakLightning : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alarak Lightning");
        }

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 200;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 69;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1000;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            if (target.whoAmI != projectile.ai[0])
            {
                if (projectile.ai[1] == 3 || projectile.ai[1] == -1)
                {
                    damage = (int) (damage * 2.5);
                }
                else damage *= 2;

                if (projectile.ai[1] == 1 || projectile.ai[1] == -1)
                {
                    var player = Main.player[projectile.owner];
                    if (damage != 0 && player.lifeSteal > 0f && !player.moonLeech)
                    {
                        var amount = Math.Min(4, player.statLifeMax2 - player.statLife);
                        if (amount == 0)
                        {
                            return;
                        }

                        player.lifeSteal -= amount;
                        player.HealEffect(amount);
                        player.statLife += amount;
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.player[projectile.owner].gravControl2)
            {
                projectile.localNPCImmunity[target.whoAmI] = 5;
            }

            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void AI()
        {
            if (projectile.alpha == 69)
            {
                Main.PlaySound(SoundLoader.customSoundType, (int) projectile.position.X, (int) projectile.position.Y,
                    mod.GetSoundSlot(SoundType.Custom, "Sounds/LightningSurge2"), 1f, Main.rand.NextFloat(-.1f, .1f));
                projectile.alpha = 255;
            }

            if (Main.rand.NextFloat(projectile.velocity.Length(), 10) > 5.5f)
            {
                var slopeX = (Main.rand.NextDouble() - .5) * 5f;
                var slopeY = (Main.rand.NextDouble() - .5) * 5f;
                for (var i = -5; i <= 5; i++)
                {
                    var dust = Dust.NewDustPerfect(
                        new Vector2((float) (projectile.position.X + slopeX * i),
                            (float) (projectile.position.Y + slopeY * i)), 182);
                    dust.noGravity = true;
                    dust.velocity = new Vector2(0, 0);
                }
            }
        }
    }
}