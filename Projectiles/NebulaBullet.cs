using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace DoomBubblesMod.Projectiles
{
    public class NebulaBullet : LunarBullet
    {
        public override int DustType => mod.DustType("Nebula229");

        private readonly float Distance = 75f;
        
        public override void AI()
        {
            base.AI();
            NPC target = null;
            for (var i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && nPC.immune[projectile.owner] == 0 && projectile.localNPCImmunity[i] == 0 && nPC.CanBeChasedBy(this) && nPC.Hitbox.Distance(projectile.Center) < Distance)
                {
                    target = nPC;
                    break;
                }
            }

            if (target != null)
            {
                Vector2 newPos = new Vector2();
                
                bool leftLeft = target.Hitbox.Left < projectile.Center.X;
                bool rightRight = target.Hitbox.Right > projectile.Center.X;
                bool upUp = target.Hitbox.Top < projectile.Center.Y;
                bool downDown = target.Hitbox.Bottom > projectile.Center.Y;

                if (leftLeft && rightRight)
                {
                    newPos.X = projectile.Center.X;
                } else if (leftLeft)
                {
                    newPos.X = target.Hitbox.Right - 5;
                } else if (rightRight)
                {
                    newPos.X = target.Hitbox.Left + 5;
                }
                if (upUp && downDown)
                {
                    newPos.Y = projectile.Center.Y;
                } else if (upUp)
                {
                    newPos.Y = target.Hitbox.Bottom - 5;
                } else if (downDown)
                {
                    newPos.Y = target.Hitbox.Top + 5;
                }

                double newDX = projectile.velocity.Length() * Math.Cos((newPos - projectile.Center).ToRotation());
                double newDY = projectile.velocity.Length() * Math.Sin((newPos - projectile.Center).ToRotation());

                for (double theta = 0; theta < Math.PI * 2; theta += Math.PI / 12)
                {
                    int dust1 = Dust.NewDust(
                        projectile.Center + new Vector2((float) (10 * Math.Cos(theta)), (float) (10 * Math.Sin(theta))),
                        0, 0, DustType, (float) Math.Cos(theta + Math.PI), (float) Math.Sin(theta + Math.PI));
                    Main.dust[dust1].noGravity = true;
                    
                    int dust2 = Dust.NewDust(newPos,0, 0, DustType, (float) Math.Cos(theta), (float) Math.Sin(theta));
                    Main.dust[dust2].noGravity = true;
                }

                projectile.Center = newPos;
                projectile.velocity = new Vector2((float) newDX, (float) newDY);

            }
        }
    }
}