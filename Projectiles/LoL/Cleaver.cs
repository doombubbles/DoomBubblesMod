using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class Cleaver : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cleaver");
        }

        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.alpha = 100;
            projectile.light = 0.5f;
            projectile.aiStyle = 18;
            projectile.friendly = true;
            projectile.penetrate = 7;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.timeLeft = 180;
            projectile.scale = 1.3f;
            aiType = 274;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.timeLeft < 85)
            {
                byte b2 = (byte)(projectile.timeLeft * 3);
                byte a2 = (byte)(100f * ((float)b2 / 255f));
                return new Color((int)b2, (int)b2, (int)b2, (int)a2);
            }
            return new Color(255, 255, 255, 127);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (Main.rand.Next(5) == 0)
            {
                if (projectile.timeLeft < 85)
                {
                    byte b2 = (byte)(projectile.timeLeft * 3);
                    byte a2 = (byte)(100f * ((float)b2 / 255f));
                    int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 60, 0, 0, 255 - a2);
                }
                else
                {
                    int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 60, 0, 0, 0);
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            projectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().cleaving.Clear();
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(mod.BuffType("Cleaved"), 300, false);
            if (target.GetGlobalNPC<DoomBubblesGlobalNPC>().Cleaved < 6 && !projectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().cleaving.Contains(target.whoAmI))
            {
                
                target.GetGlobalNPC<DoomBubblesGlobalNPC>().Cleaved += 1;
                projectile.GetGlobalProjectile<DoomBubblesGlobalProjectile>().cleaving.Add(target.whoAmI);
                
                if (Main.netMode == 1)
                {
                    ModPacket packet = mod.GetPacket();
                    packet.Write((byte)DoomBubblesModMessageType.cleaved);
                    packet.Write(target.whoAmI);
                    packet.Send();
                    ModPacket packet2 = mod.GetPacket();
                    packet2.Write((byte)DoomBubblesModMessageType.cleaving);
                    packet2.Write(target.whoAmI);
                    packet2.Write(projectile.whoAmI);
                    packet2.Send();
                }
                
            }
            
            
        }
    }
}