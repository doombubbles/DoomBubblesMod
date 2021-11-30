using System;
using DoomBubblesMod.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace DoomBubblesMod.Projectiles.HotS
{
    public class DiscordStrike : CenteredProjectile
    {
        private int Size => (int) (Math.Sqrt(Projectile.velocity.Length() * (ChosenTalent == 2 || ChosenTalent == -1 ? 1.5f : 3f)) * Math.Sqrt(60));
        private float Length => Projectile.velocity.Length() * 15f;
        private int ChosenTalent => (int) Math.Round(Projectile.ai[0]);
        private float DistanceFactor => (Origin - Projectile.Center).Length() / Length;

        private Vector2 Origin
        {
            get => new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
            set
            {
                Projectile.localAI[0] = value.X;
                Projectile.localAI[1] = value.Y;
            }
        }

        private float Theta
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public override void SetDefaults()
        {
            Projectile.width = Size;
            Projectile.height = Size;
            Projectile.DamageType = GetInstance<MeleeMagic>();
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 69;
            Projectile.timeLeft = 100;
            Projectile.extraUpdates = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1000;
            Projectile.penetrate = -1;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            if (ChosenTalent == 3 || ChosenTalent == -1)
            {
                var missingHp = target.lifeMax - target.life;
                damage += (int) (damage * ((float) missingHp / target.lifeMax) + missingHp * .02);
            }

            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
            if (ChosenTalent == 1 || ChosenTalent == -1)
            {
                Projectile.damage = (int) (Projectile.damage * 1.1);
            }
        }

        public override void AI()
        {
            if (Projectile.alpha == 69)
            {
                Projectile.alpha = 255;
                Origin = Projectile.Center;
                Theta = Projectile.velocity.ToRotation();
            }

            HandlePosition();
            HandleSize();
            CreateDust();
        }

        private void HandlePosition()
        {
            //Projectile.position += Main.player[Projectile.owner].velocity;
            //Origin += Main.player[Projectile.owner].velocity;
        }

        private void HandleSize()
        {
            var center = Projectile.Center;
            Projectile.width = (int) (Size * (1 - DistanceFactor));
            Projectile.height = (int) (Size * (1 - DistanceFactor));
            Projectile.scale = 1 - DistanceFactor;
            Projectile.Center = center;

            if (Projectile.width <= 0 || Projectile.height <= 0)
            {
                Projectile.Kill();
            }
        }

        private void CreateDust()
        {
            var line1Start = new Vector2((float) (Origin.X + Size * Math.Cos(Theta + Math.PI / 2) / 2),
                (float) (Origin.Y + Size * Math.Sin(Theta + Math.PI / 2) / 2));
            var line2Start = new Vector2((float) (Origin.X + Size * Math.Cos(Theta - Math.PI / 2) / 2),
                (float) (Origin.Y + Size * Math.Sin(Theta - Math.PI / 2) / 2));

            var dot = line1Start;
            float previousDistance;
            do
            {
                previousDistance = (dot - Projectile.Center).Length();
                var dust = Dust.NewDustPerfect(dot, 182);
                dust.noGravity = true;
                dust.velocity = new Vector2(0, 0);
                dot.X += (float) Math.Cos((Projectile.Center - dot).ToRotation()) * 2f;
                dot.Y += (float) Math.Sin((Projectile.Center - dot).ToRotation()) * 2f;
            } while ((dot - Projectile.Center).Length() < previousDistance);

            dot = line2Start;
            do
            {
                previousDistance = (dot - Projectile.Center).Length();
                var dust = Dust.NewDustPerfect(dot, 182);
                dust.noGravity = true;
                dust.velocity = new Vector2(0, 0);
                dot.X += (float) Math.Cos((Projectile.Center - dot).ToRotation()) * 2f;
                dot.Y += (float) Math.Sin((Projectile.Center - dot).ToRotation()) * 2f;
            } while ((dot - Projectile.Center).Length() < previousDistance);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            if (Main.player[Projectile.owner].gravControl2)
            {
                Projectile.localNPCImmunity[target.whoAmI] = 0;
                target.immune[Projectile.owner] = 0;
            }
        }
    }
}