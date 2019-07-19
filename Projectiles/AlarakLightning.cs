using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class AlarakLightning : ModProjectile
    {
        private List<int> hit = new List<int>();
        
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
            projectile.alpha = 255;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.whoAmI != projectile.ai[0])
            {
                if (projectile.ai[1] == 3 || projectile.ai[1] == -1)
                {
                    damage = (int) (damage * 2.5);
                } else damage *= 2;
                if (projectile.ai[1] == 1 || projectile.ai[1] == -1)
                {
                    Player player = Main.player[projectile.owner];
                    if (damage != 0 && player.lifeSteal > 0f && !player.moonLeech)
                    {
                        int amount = Math.Min(4, player.statLifeMax2 - player.statLife);
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
            target.immune[projectile.owner] = 0;
            hit.Add(target.whoAmI);
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (hit.Contains(target.whoAmI))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }

        public override void AI()
        {
            if (Main.rand.NextFloat(projectile.velocity.Length(), 10) > 5.5f)
            {
                var slopeX = (Main.rand.NextDouble() - .5) * 5f;
                var slopeY = (Main.rand.NextDouble() - .5) * 5f;
                for (int i = -5; i <= 5; i++)
                {
                    Dust dust = Dust.NewDustPerfect(new Vector2((float) (projectile.position.X + (slopeX * i)), (float) (projectile.position.Y + slopeY * i)), 182);
                    dust.noGravity = true;
                    dust.velocity = new Vector2(0,0);
                }
            }
            
        }
    }
}