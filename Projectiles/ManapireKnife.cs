﻿using System;
using DoomBubblesMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class ManapireKnife : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Manapire Knife");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.VampireKnife);
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                ref var referenc = ref Projectile.localAI[0];
                referenc += 1f;
                Projectile.alpha = 0;
            }

            Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f *
                                   Projectile.direction;

            ref var reference = ref Projectile.ai[0];
            reference += 1f;
            if (Projectile.ai[0] >= 30f)
            {
                Projectile.alpha += 10;
                Projectile.damage = (int) (Projectile.damage * 0.9);
                Projectile.knockBack = (int) (Projectile.knockBack * 0.9);
                if (Projectile.alpha >= 255)
                {
                    Projectile.active = false;
                }
            }

            if (Projectile.ai[0] < 30f)
            {
                Projectile.rotation = (float) Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            var player = Main.player[Projectile.owner];

            if ((player.statLife < player.statLifeMax2 && !player.moonLeech && target.FullName != "Target Dummy"
                 || player.statMana < player.statManaMax2) && target.lifeMax > 5 && damage > 0)
            {
                Projectile.NewProjectile(new ProjectileSource_ProjectileParent(Projectile), target.Center.X, target.Center.Y, 0f, 0f,
                    ModContent.ProjectileType<ManapireRestore>(), 0, 0f, Projectile.owner, Projectile.owner, damage);
            }
        }

        public override void Kill(int timeLeft)
        {
            for (var num401 = 0; num401 < 3; num401++)
            {
                var num402 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                    Projectile.height, ModContent.DustType<ManapireDust>(), 0f, 0f, 100, default, 0.8f);
                Main.dust[num402].noGravity = true;
                var dust = Main.dust[num402];
                dust.velocity *= 1.2f;
                dust = Main.dust[num402];
                dust.velocity -= Projectile.oldVelocity * 0.3f;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha,
                (byte) ((255 - Projectile.alpha) / 3f));
        }
    }
}