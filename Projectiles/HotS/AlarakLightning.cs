using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using SoundType = Terraria.ModLoader.SoundType;

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
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 200;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 69;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1000;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            if (target.whoAmI != Projectile.ai[0])
            {
                if (Projectile.ai[1] == 3 || Projectile.ai[1] == -1)
                {
                    damage = (int) (damage * 2.5);
                }
                else damage *= 2;

                if (Projectile.ai[1] == 1 || Projectile.ai[1] == -1)
                {
                    var player = Main.player[Projectile.owner];
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
            if (Main.player[Projectile.owner].gravControl2)
            {
                Projectile.localNPCImmunity[target.whoAmI] = 5;
            }

            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void AI()
        {
            if (Projectile.alpha == 69)
            {
                SoundEngine.PlaySound(SoundLoader.customSoundType, (int) Projectile.position.X, (int) Projectile.position.Y,
                    Mod.GetSoundSlot(SoundType.Custom, "Sounds/LightningSurge2"), 1f, Main.rand.NextFloat(-.1f, .1f));
                Projectile.alpha = 255;
            }

            if (Main.rand.NextFloat(Projectile.velocity.Length(), 10) > 5.5f)
            {
                var slopeX = (Main.rand.NextDouble() - .5) * 5f;
                var slopeY = (Main.rand.NextDouble() - .5) * 5f;
                for (var i = -5; i <= 5; i++)
                {
                    var dust = Dust.NewDustPerfect(
                        new Vector2((float) (Projectile.position.X + slopeX * i),
                            (float) (Projectile.position.Y + slopeY * i)), 182);
                    dust.noGravity = true;
                    dust.velocity = new Vector2(0, 0);
                }
            }
        }
    }
}