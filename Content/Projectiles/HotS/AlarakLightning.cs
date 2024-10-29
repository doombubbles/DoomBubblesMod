using System;
using DoomBubblesMod.Utils;
using Terraria;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Projectiles.HotS;

public class AlarakLightning : HotsProjectile
{
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

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (target.whoAmI != Projectile.ai[0])
        {
            if (Projectile.ai[1] == 3 || Projectile.ai[1] == -1)
            {
                modifiers.SourceDamage *= 2.5f;
            }
            else
            {
                modifiers.SourceDamage *= 2f;
            }
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Main.player[Projectile.owner].gravControl2)
        {
            Projectile.localNPCImmunity[target.whoAmI] = 5;
        }

        if (Projectile.ai[1] == 1 || Projectile.ai[1] == -1)
        {
            var player = Main.player[Projectile.owner];
            if (damageDone > 0 && player.lifeSteal > 0f && !player.moonLeech)
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
        
        base.OnHitNPC(target, hit, damageDone);
    }

    public override void AI()
    {
        if (Projectile.alpha == 69)
        {
            SoundEngine.PlaySound(Mod.Sound("LightningSurge2"), Projectile.position);
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