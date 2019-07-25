using System;
using DoomBubblesMod.Items.Thanos;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.Thanos
{
	public class RealityBeam : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reality Beam");
        }

        public override void SetDefaults()
		{
            projectile.width = 16;
            projectile.height = 16;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 60;
			projectile.hide = true;
			projectile.damage = 100;
		}

        public override void AI()
        {
	        Player player = Main.player[projectile.owner];
	        Vector2 gauntlet = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);
	        
	        handleProjectiles(gauntlet);
	        
	        if (Math.Sqrt(Math.Pow(gauntlet.X - projectile.position.X, 2) + Math.Pow(gauntlet.Y - projectile.position.Y, 2)) < 30f)
	        {
		        projectile.Kill();
		        return;
	        }
	        
	        for (int i = 0; i < 6; i++)
	        {
		        Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X + (projectile.velocity.X * i / 6), 
			        projectile.Center.Y + (projectile.velocity.Y * i / 6)), 0, 0, 212, 0, 0, 0, InfinityGauntlet.reality, 1.5f)];
		        dust.velocity *= .5f;
		        dust.noGravity = true;
		        dust.customData = "Reality Beam";
	        }
	        /*
	        Vector2 vector = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
	        if (player.direction != 1)
	        {
		        vector.X = player.bodyFrame.Width - vector.X;
	        }
	        if (player.gravDir != 1f)
	        {
		        vector.Y = player.bodyFrame.Height - vector.Y;
	        }
	        vector -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
	        Vector2 goTowards = player.RotatedRelativePoint(player.position + vector) - player.velocity;
			*/


	        double dXPlayer = gauntlet.X - projectile.Center.X;
	        double dYPlayer = gauntlet.Y - projectile.Center.Y;
	        double theta = Math.Atan2(dYPlayer, dXPlayer);
	        
	        projectile.velocity.X = (float) (40 * Math.Cos(theta));
	        projectile.velocity.Y = (float) (40 * Math.Sin(theta));
	        
	        
	        
	        
        }

		public void handleProjectiles(Vector2 gauntlet)
		{
			foreach (Projectile otherProjectile in Main.projectile)
			{
				if (otherProjectile.Distance(projectile.Center) < 16f && !otherProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>(mod).realityStone
				    && otherProjectile.type != mod.ProjectileType("RealityBeam") && (!otherProjectile.friendly || otherProjectile.hostile))
				{
					otherProjectile.friendly = true;
					otherProjectile.hostile = false;
					Vector2 mousePos = Main.MouseWorld;
					double theta2 = Math.Atan2(mousePos.Y - gauntlet.Y , mousePos.X - gauntlet.X);
					otherProjectile.Center = gauntlet;
					double speed = Math.Sqrt(Math.Pow(otherProjectile.velocity.X, 2) + Math.Pow(otherProjectile.velocity.Y, 2));
					otherProjectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>(mod).realityStone = true;
					otherProjectile.velocity = new Vector2((float) (Math.Max(1.5 * speed, 5) * Math.Cos(theta2)), 
						(float) (Math.Max(1.5 * speed, 5) * Math.Sin(theta2)));
					
				}
			}
		}
		
		

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Confused, 60 * Main.rand.Next(5, 10), false);
			target.AddBuff(BuffID.Ichor, 60 * Main.rand.Next(5, 10), false);
			target.AddBuff(BuffID.Venom, 60 * Main.rand.Next(5, 10), false);
			target.AddBuff(BuffID.OnFire, 60 * Main.rand.Next(5, 10), false);
			target.AddBuff(BuffID.CursedInferno, 60 * Main.rand.Next(5, 10), false);
			target.AddBuff(BuffID.Frostburn, 60 * Main.rand.Next(5, 10), false);
			target.immune[projectile.owner] = 1;
			base.OnHitNPC(target, damage, knockback, crit);
		}
	}
}