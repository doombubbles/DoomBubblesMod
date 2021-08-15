using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using SoundType = Terraria.ModLoader.SoundType;

namespace DoomBubblesMod.Projectiles.HotS
{
    public class Photon : CenteredProjectile
    {
        public int ChosenTalent => (int) Math.Round(Projectile.ai[0]);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Photon");
            Main.projFrames[Projectile.type] = 5;
            ProjectileID.Sets.CountsAsHoming[Projectile.type] = true;
            ProjectileID.Sets.MinionShot[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.scale = .6f;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = -1;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            if (target.whoAmI == (int) Projectile.ai[1])
            {
                Projectile.penetrate = 1;
                SoundEngine.PlaySound(SoundLoader.customSoundType, (int) Projectile.position.X, (int) Projectile.position.Y,
                    Mod.GetSoundSlot(SoundType.Custom, "Sounds/PhotonHit"));
            }

            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void AI()
        {
            Projectile.frame++;
            if (Projectile.frame > 4)
            {
                Projectile.frame = 0;
            }

            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }

            Projectile.localAI[0] = Projectile.velocity.Length() * 1.02f;
            Homing();
        }


        public void Homing()
        {
            var target = Main.npc[(int) Projectile.ai[1]];
            if (!target.active)
            {
                Projectile.Kill();
            }

            var x = Projectile.Center.X;
            var y = Projectile.Center.Y - 6;
            var theta = Math.Atan2(target.Center.Y - y, target.Center.X - x);
            var dX = Projectile.localAI[0] * Math.Cos(theta);
            var dY = Projectile.localAI[0] * Math.Sin(theta);
            Projectile.velocity = new Vector2((float) dX, (float) dY);
        }
    }
}