using System;
using Terraria.Audio;
using Terraria.ID;

namespace DoomBubblesMod.Content.Projectiles.Ranged.AllTheBullets;

public class MeowmereBullet : AllTheBullet
{
    protected override short SourceItem => ItemID.Meowmere;

    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.penetrate = 5;
        Projectile.usesLocalNPCImmunity = true;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.ai[1] += 1f;
        SoundEngine.PlaySound(SoundID.Meowmere with {Volume = .5f}, Projectile.position);
        if (Projectile.ai[1] >= 5f)
        {
            Projectile.position += Projectile.velocity;
            Projectile.Kill();
        }
        else
        {
            if (Projectile.velocity.Y != Projectile.oldVelocity.Y)
                Projectile.velocity.Y = 0f - Projectile.oldVelocity.Y;

            if (Projectile.velocity.X != Projectile.oldVelocity.X)
                Projectile.velocity.X = 0f - Projectile.oldVelocity.X;
        }

        var spinningpoint = new Vector2(0f, -3f - Projectile.ai[1]).RotatedByRandom(3.1415927410125732);
        var num16 = 10f + Projectile.ai[1] * 4f;
        var value6 = new Vector2(1.05f, 1f);
        for (var num17 = 0f; num17 < num16; num17 += 2f)
        {
            var num18 = Dust.NewDust(Projectile.Center, 0, 0, DustID.RainbowTorch, 0f, 0f, 0, Color.Transparent);
            Main.dust[num18].position = Projectile.Center;
            Main.dust[num18].velocity = spinningpoint.RotatedBy((float) Math.PI * 2f * num17 / num16) *
                                        value6 *
                                        (0.8f + Main.rand.NextFloat() * 0.4f);
            Main.dust[num18].color = Main.hslToRgb(num17 / num16, 1f, 0.5f);
            Main.dust[num18].noGravity = true;
            Main.dust[num18].scale = 1f + Projectile.ai[1] / 3f;
        }

        if (Main.myPlayer == Projectile.owner)
        {
            var width = Projectile.width;
            var height = Projectile.height;
            var num19 = Projectile.penetrate;
            Projectile.position = Projectile.Center;
            Projectile.width = Projectile.height = 40 + 8 * (int) Projectile.ai[1];
            Projectile.Center = Projectile.position;
            Projectile.penetrate = -1;
            Projectile.Damage();
            Projectile.penetrate = num19;
            Projectile.position = Projectile.Center;
            Projectile.width = width;
            Projectile.height = height;
            Projectile.Center = Projectile.position;
        }

        return false;
    }
}