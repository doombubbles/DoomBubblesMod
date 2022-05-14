using System;
using DoomBubblesMod.Content.Dusts;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class HampireRestore : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.VampireHeal);
        Projectile.aiStyle = -1;
    }

    public override void AI()
    {
        var healTarget = (int) Projectile.ai[0];
        var speed = 4f;
        var vector31 = new Vector2(Projectile.position.X + Projectile.width * 0.5f,
            Projectile.position.Y + Projectile.height * 0.5f);
        var dX = Main.player[healTarget].Center.X - vector31.X;
        var dY = Main.player[healTarget].Center.Y - vector31.Y;
        var distance = (float) Math.Sqrt(dX * dX + dY * dY);
        if (distance < 50f &&
            Projectile.position.X < Main.player[healTarget].position.X + Main.player[healTarget].width &&
            Projectile.position.X + Projectile.width > Main.player[healTarget].position.X &&
            Projectile.position.Y < Main.player[healTarget].position.Y + Main.player[healTarget].height &&
            Projectile.position.Y + Projectile.height > Main.player[healTarget].position.Y)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                var player = Main.player[healTarget];
                var amount = (int) Projectile.ai[1];

                if (!player.HasBuff(BuffID.WellFed))
                {
                    player.AddBuff(BuffID.WellFed, 1);
                }

                if (player.buffTime[player.FindBuffIndex(BuffID.WellFed)] < 3600)
                {
                    player.buffTime[player.FindBuffIndex(BuffID.WellFed)] += amount;
                    if (player.buffTime[player.FindBuffIndex(BuffID.WellFed)] > 3700)
                    {
                        player.buffTime[player.FindBuffIndex(BuffID.WellFed)] = 3700;
                    }
                    else if (Main.rand.NextBool())
                    {
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Pig1"));
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Pig2"));
                    }
                }
                else if (!Main.player[Main.myPlayer].moonLeech &&
                         player.statLife < player.statLifeMax2 &&
                         !(player.lifeSteal <= 0f))
                {
                    //life restore
                    amount = (int) (amount * 0.075f);
                    if (amount != 0)
                    {
                        player.lifeSteal -= amount;
                        player.HealEffect(amount, false);
                        player.statLife += amount;
                        if (Main.player[healTarget].statLife > Main.player[healTarget].statLifeMax2)
                        {
                            Main.player[healTarget].statLife = Main.player[healTarget].statLifeMax2;
                        }

                        NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, healTarget, amount);
                    }
                }
            }

            Projectile.Kill();
        }

        distance = speed / distance;
        dX *= distance;
        dY *= distance;
        Projectile.velocity.X = (Projectile.velocity.X * 15f + dX) / 16f;
        Projectile.velocity.Y = (Projectile.velocity.Y * 15f + dY) / 16f;

        int num4;
        for (var num500 = 0; num500 < 3; num500 = num4 + 1)
        {
            var num501 = Projectile.velocity.X * 0.334f * num500;
            var num502 = (0f - Projectile.velocity.Y * 0.334f) * num500;
            var num503 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                Projectile.height, DustType<HampireDust2>(), 0f, 0f, 100, default, 1.1f);
            Main.dust[num503].noGravity = true;
            var dust3 = Main.dust[num503];
            dust3.velocity *= 0f;
            var dust70 = Main.dust[num503];
            dust70.position.X = dust70.position.X - num501;
            var dust71 = Main.dust[num503];
            dust71.position.Y = dust71.position.Y - num502;
            num4 = num500;
        }
    }
}