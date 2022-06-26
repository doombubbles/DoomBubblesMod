using System;
using ElementalDamage.Content.DamageClasses;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class TrueHomingBullet : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 8;
        Projectile.height = 8;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = GetInstance<RangedHoly>();
        Projectile.penetrate = 1;
        Projectile.timeLeft = 600;
        Projectile.alpha = 255;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        Projectile.extraUpdates = 1;
        Projectile.light = 0.3f;
        AIType = ProjectileID.Bullet;
    }

    public override bool PreAI()
    {
        var projSpeed = (float) Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X +
                                          Projectile.velocity.Y * Projectile.velocity.Y);
        var num139 = Projectile.localAI[1];
        if (num139 == 0f)
        {
            Projectile.localAI[1] = projSpeed;
            num139 = projSpeed;
        }

        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= 25;
        }

        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }

        var initialX = Projectile.position.X;
        var initialY = Projectile.position.Y;
        var homingDistance = 300f;
        var flag4 = false;
        var num143 = 0;
        if (Projectile.ai[1] == 0f)
        {
            for (var n = 0; n < 200; n++)
            {
                if (Main.npc[n].CanBeChasedBy(Projectile) && (Projectile.ai[1] == 0f || Projectile.ai[1] == n + 1))
                {
                    var npcCenterX = Main.npc[n].position.X + Main.npc[n].width / 2;
                    var npcCenterY = Main.npc[n].position.Y + Main.npc[n].height / 2;
                    var distanceDifference = Math.Abs(Projectile.position.X + Projectile.width / 2 - npcCenterX) +
                                             Math.Abs(Projectile.position.Y + Projectile.height / 2 - npcCenterY);
                    if (distanceDifference < homingDistance &&
                        Collision.CanHit(
                            new Vector2(Projectile.position.X + Projectile.width / 2,
                                Projectile.position.Y + Projectile.height / 2), 1, 1, Main.npc[n].position,
                            Main.npc[n].width, Main.npc[n].height))
                    {
                        homingDistance = distanceDifference;
                        initialX = npcCenterX;
                        initialY = npcCenterY;
                        flag4 = true;
                        num143 = n;
                    }
                }
            }

            if (flag4)
            {
                Projectile.ai[1] = num143 + 1;
            }

            flag4 = false;
        }

        if (Projectile.ai[1] > 0f)
        {
            var num148 = (int) (Projectile.ai[1] - 1f);
            if (Main.npc[num148].active &&
                Main.npc[num148].CanBeChasedBy(Projectile, true) &&
                !Main.npc[num148].dontTakeDamage)
            {
                var num149 = Main.npc[num148].position.X + Main.npc[num148].width / 2;
                var num150 = Main.npc[num148].position.Y + Main.npc[num148].height / 2;
                var num151 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num149) +
                             Math.Abs(Projectile.position.Y + Projectile.height / 2 - num150);
                if (num151 < 1000f)
                {
                    flag4 = true;
                    initialX = Main.npc[num148].position.X + Main.npc[num148].width / 2;
                    initialY = Main.npc[num148].position.Y + Main.npc[num148].height / 2;
                }
            }
            else
            {
                Projectile.ai[1] = 0f;
            }
        }

        if (!Projectile.friendly)
        {
            flag4 = false;
        }

        if (flag4)
        {
            var num152 = num139;
            var vector13 = new Vector2(Projectile.position.X + Projectile.width * 0.5f,
                Projectile.position.Y + Projectile.height * 0.5f);
            var num153 = initialX - vector13.X;
            var num154 = initialY - vector13.Y;
            var num155 = (float) Math.Sqrt(num153 * num153 + num154 * num154);
            num155 = num152 / num155;
            num153 *= num155;
            num154 *= num155;
            var num156 = 8;
            Projectile.velocity.X = (Projectile.velocity.X * (num156 - 1) + num153) / num156;
            Projectile.velocity.Y = (Projectile.velocity.Y * (num156 - 1) + num154) / num156;
        }

        return base.PreAI();
    }

    public override void Kill(int timeLeft)
    {
        // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
        Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width,
            Projectile.height);
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
    }
}