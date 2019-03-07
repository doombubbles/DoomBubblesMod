using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace DoomBubblesMod.Projectiles
{
    public class Skelmourne : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skelmourne");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.alpha = 100;
            projectile.light = 0.5f;
            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 3;
            projectile.tileCollide = false;
            aiType = 27;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            int num7 = (int)((projectile.localAI[1] - 5f) / 10f * 255f);
            return new Color(num7, num7, num7, num7);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>().frostmourne >= 100)
            {
                return;
            }
            target.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).frostmournedmg += Math.Min(damage, target.life);
            if (Main.netMode == 1)
            {
                ModPacket packet = mod.GetPacket();
                packet.Write((byte)DoomBubblesModMessageType.frostmournedmg);
                packet.Write(target.whoAmI);
                packet.Write(target.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).frostmournedmg);
                packet.Send();
            }
        }

        public override void AI()
        {
            /*
            if (projectile.localAI[1] == 1)
            {
                float mouseX = 0;
                float mouseY = 0;
                if (projectile.owner == Main.myPlayer)
                {
                    mouseX = (Main.mouseX - Main.screenWidth / 2) + Main.player[Main.myPlayer].position.X;
                    mouseY = (Main.mouseY - Main.screenHeight / 2) + Main.player[Main.myPlayer].position.Y;
                }

                float numX = projectile.position.X - mouseX + Main.rand.Next(-50, 50);
                float numY = projectile.position.Y - mouseY;

                double angle = -Math.Atan2(numX, numY) - Math.PI / 2;

                projectile.velocity.X = 20 * (float)Math.Cos(angle);
                projectile.velocity.Y = 20 * (float)Math.Sin(angle);
            }
            */
            if (projectile.localAI[1] > 7f)
            {
                int dust1 = Dust.NewDust(new Vector2(projectile.position.X - 40 * (float)Math.Cos(projectile.rotation - Math.PI / 4), projectile.position.Y - 40 * (float)Math.Sin(projectile.rotation - Math.PI / 4)), 12, 12, 15, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.5f);
                //int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 12, 12, 15, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.5f);
                Main.dust[dust1].velocity *= -0.25f;
                Main.dust[dust1].noGravity = true;
            }
            if (projectile.position.Y > Main.player[projectile.owner].position.Y + 50)
            {
                projectile.tileCollide = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
            int num9;
            for (int num689 = 4; num689 < 31; num689 = num9 + 1)
            {
                float num690 = projectile.oldVelocity.X * (35f / (float)num689);
                float num691 = projectile.oldVelocity.Y * (35f / (float)num689);
                int num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 6, 6, 15, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.8f);
                Main.dust[num692].noGravity = true;
                Dust dust = Main.dust[num692];
                dust.velocity *= 0.5f;
                num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 6, 6, 15, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.4f);
                dust = Main.dust[num692];
                dust.velocity *= 0.05f;
                Main.dust[num692].noGravity = true;
                num9 = num689;
            }
            return base.OnTileCollide(oldVelocity);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                int num9;
                for (int num689 = 4; num689 < 31; num689 = num9 + 1)
                {
                    float num690 = projectile.oldVelocity.X * (35f / (float)num689);
                    float num691 = projectile.oldVelocity.Y * (35f / (float)num689);
                    int num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 6, 6, 15, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.8f);
                    Main.dust[num692].noGravity = true;
                    Dust dust = Main.dust[num692];
                    dust.velocity *= 0.5f;
                    num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 6, 6, 15, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.4f);
                    dust = Main.dust[num692];
                    dust.velocity *= 0.05f;
                    Main.dust[num692].noGravity = true;
                    num9 = num689;
                }
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                int num9;
                for (int num689 = 4; num689 < 31; num689 = num9 + 1)
                {
                    float num690 = projectile.oldVelocity.X * (35f / (float)num689);
                    float num691 = projectile.oldVelocity.Y * (35f / (float)num689);
                    int num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 6, 6, 15, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.8f);
                    Main.dust[num692].noGravity = true;
                    Dust dust = Main.dust[num692];
                    dust.velocity *= 0.5f;
                    num692 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num690, projectile.oldPosition.Y - num691), 6, 6, 15, projectile.oldVelocity.X, projectile.oldVelocity.Y, 5, default(Color), 1.4f);
                    dust = Main.dust[num692];
                    dust.velocity *= 0.05f;
                    Main.dust[num692].noGravity = true;
                    num9 = num689;
                }
            }
            base.OnHitPvp(target, damage, crit);
        }
    }
}