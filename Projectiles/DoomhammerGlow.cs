using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace DoomBubblesMod.Projectiles
{
	public class DoomhammerGlow : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomhammer");
        }

		public override void SetDefaults()
		{
            projectile.width = 176;
            projectile.height = 176;
            projectile.light = 0.5f;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.timeLeft = 180;
		}

        public override void AI()
        {
            float pi = (float)Math.PI;
            int rot = Main.player[projectile.owner].direction;
            if (projectile.timeLeft == 180)
            {
                
                projectile.timeLeft = (int)(projectile.ai[0] * .75f);


                if (rot == -1) projectile.rotation = (.25f * pi);
                else projectile.rotation = ((pi) / -2) - (.25f * pi);
                
                //projectile.rotation = ((-.25f * (float)Math.PI) + (rot * (.5f * (float)Math.PI)));
            }
            projectile.position.X = (Main.player[projectile.owner].position.X - 77) + (5 * rot);
            projectile.position.Y = (Main.player[projectile.owner].position.Y - 70) - 5;
            projectile.rotation += (rot * (1.25f * (pi) / projectile.ai[0]));
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 20; i++)
            {
                int dust = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 6);
            }
        }
    }
}