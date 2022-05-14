using System;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Buffs;
using Terraria.Audio;
using Terraria.GameContent;

namespace DoomBubblesMod.Content.Projectiles.HotS;

public class Repeater : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 5;
        Projectile.height = 5;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.alpha = 69;
        Projectile.extraUpdates = 2;
        Projectile.scale = 1f;
        Projectile.timeLeft = 600;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ignoreWater = true;
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        if (Projectile.owner == Main.myPlayer)
        {
            var player = Main.player[Projectile.owner];
            player.AddBuff(BuffType<FenixBombBuildUp>(), 360);
            if ((Projectile.ai[1] == 3 || Projectile.ai[1] == -1) &&
                player.GetModPlayer<HotSPlayer>().fenixBombBuildUp == 14 ||
                !(Projectile.ai[1] == 3 || Projectile.ai[1] == -1) &&
                player.GetModPlayer<HotSPlayer>().fenixBombBuildUp == 9)
            {
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Boung"), Projectile.position);
            }

            player.GetModPlayer<HotSPlayer>().fenixBombBuildUp++;
            if (Projectile.ai[1] == 3 || Projectile.ai[1] == -1)
            {
                if (player.GetModPlayer<HotSPlayer>().fenixBombBuildUp > 15)
                {
                    player.GetModPlayer<HotSPlayer>().fenixBombBuildUp = 15;
                }
            }
            else if (player.GetModPlayer<HotSPlayer>().fenixBombBuildUp > 10)
            {
                player.GetModPlayer<HotSPlayer>().fenixBombBuildUp = 10;
            }
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        var originX = (TextureAssets.Projectile[Projectile.type].Value.Width - Projectile.width) * 0.5f +
                      Projectile.width * 0.5f;
        var offsetX = 0;
        var offsetY = 0;
        var spriteEffects = SpriteEffects.None;
        var color25 = Lighting.GetColor((int) (Projectile.position.X + Projectile.width * 0.5) / 16,
            (int) ((Projectile.position.Y + Projectile.height * 0.5) / 16.0));

        var value11 = new Rectangle((int) Main.screenPosition.X - 500, (int) Main.screenPosition.Y - 500,
            Main.screenWidth + 1000, Main.screenHeight + 1000);
        if (Projectile.getRect().Intersects(value11))
        {
            var value12 = new Vector2(Projectile.position.X - Main.screenPosition.X + originX + offsetX,
                Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2f + Projectile.gfxOffY);
            var num152 = 100f;
            var scaleFactor = 3f;
            for (var num153 = 1; num153 <= (int) Projectile.localAI[0]; num153++)
            {
                var value13 = Vector2.Normalize(Projectile.velocity) * num153 * scaleFactor;
                var alpha2 = Projectile.GetAlpha(lightColor);
                alpha2 *= (num152 - num153) / num152;
                alpha2.A = 0;
                Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, value12 - value13, null, alpha2,
                    Projectile.rotation, new Vector2(originX, Projectile.height / 2 + offsetY), Projectile.scale,
                    spriteEffects, 0);
            }
        }

        return false;
    }

    public override void AI()
    {
        if (Projectile.alpha == 69)
        {
            Projectile.alpha = 255;
            SoundEngine.PlaySound(
                SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Repeater" + Math.Min(3, Projectile.ai[0])),
                Projectile.position);
            /*SoundEngine.PlaySound(SoundLoader.customSoundType, (int) Projectile.position.X, (int) Projectile.position.Y,
                Mod.GetSoundSlot(SoundType.Custom, "Sounds/Repeater" + Math.Min(3, Projectile.ai[0])), 1f,
                Projectile.ai[0] == 4 ? -.25f : 0f);*/
        }

        Projectile.ai[0] = 0;

        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= 25;
        }

        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }

        Lighting.AddLight((int) Projectile.Center.X / 16, (int) Projectile.Center.Y / 16, 0.2f, 0.2f, 0.25f);

        var num55 = 50f;
        var num56 = 3f;

        Projectile.localAI[0] += num56;
        if (Projectile.localAI[0] > num55)
        {
            Projectile.localAI[0] = num55;
        }
    }

    public override void Kill(int timeLeft)
    {
        var num293 = Main.rand.Next(3, 7);
        for (var num294 = 0; num294 < num293; num294++)
        {
            var num295 = Dust.NewDust(Projectile.Center - Projectile.velocity / 2f, 0, 0, DustID.WhiteTorch, 0f, 0f,
                100,
                Color.Lavender, 2.1f);
            var dust = Main.dust[num295];
            dust.velocity *= 2f;
            Main.dust[num295].noGravity = true;
        }

        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Hit"), Projectile.position);
        base.Kill(timeLeft);
    }
}