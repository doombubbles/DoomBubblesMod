using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.HotS
{
    public class Photon : HappyProjectile
    {
        public int ChosenTalent => (int) Math.Round(projectile.ai[0]);
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Photon");
            Main.projFrames[projectile.type] = 5;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionShot[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.scale = .6f;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
            projectile.aiStyle = -1;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.whoAmI == (int) projectile.ai[1])
            {
                projectile.penetrate = 1;
                Main.PlaySound(SoundLoader.customSoundType, (int) projectile.position.X, (int) projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/PhotonHit"));
            }
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void AI()
        {
            projectile.frame++;
            if (projectile.frame > 4)
            {
                projectile.frame = 0;
            }

            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }

            projectile.localAI[0] = projectile.velocity.Length() * 1.02f;
            Homing();
        }


        public void Homing()
        {
            NPC target = Main.npc[(int) projectile.ai[1]];
            if (!target.active)
            {
                projectile.Kill();
            }
            float x = projectile.Center.X;
            float y = projectile.Center.Y - 6;
            double theta = Math.Atan2(target.Center.Y - y, target.Center.X - x);
            double dX = projectile.localAI[0] * Math.Cos(theta);
            double dY = projectile.localAI[0] * Math.Sin(theta);
            projectile.velocity = new Vector2((float) dX, (float) dY);
        }
    }
}