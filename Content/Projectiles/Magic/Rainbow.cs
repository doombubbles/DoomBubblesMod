using ElementalDamage.Content.DamageClasses;
using Terraria.GameContent;
using Terraria.Graphics;

namespace DoomBubblesMod.Content.Projectiles.Magic;

public class Rainbow : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 5;
        Projectile.height = 5;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.alpha = 255;
        Projectile.extraUpdates = 2;
        Projectile.scale = 1f;
        Projectile.timeLeft = 600;
        Projectile.DamageType = GetInstance<MagicHoly>();
        Projectile.ignoreWater = true;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.light = .3f;
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
                Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2 + Projectile.gfxOffY);
            var num152 = 100f;
            var scaleFactor = 3f;
            for (var num153 = 1; num153 <= (int) Projectile.ai[1]; num153++)
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

    public override void PostDraw(Color lightColor)
    {
        
        new RainbowRodDrawer().Draw(Projectile);

    }

    public override void AI()
    {
        Projectile.ai[0] = 0;

        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= 25;
        }

        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }

        var num55 = 100f;
        var num56 = 3f;

        Projectile.ai[1] += num56;
        if (Projectile.ai[1] > num55)
        {
            Projectile.ai[1] = num55;
        }
    }

    public override void Kill(int timeLeft)
    {
        var num293 = Main.rand.Next(3, 7);
        for (var num294 = 0; num294 < num293; num294++)
        {
            var num295 = Dust.NewDust(Projectile.Center - Projectile.velocity / 2f, 0, 0, DustID.RainbowMk2, 0f, 0f,
                100, RainbowColors[Main.rand.Next(0, 6)], 1.5f);
            var dust = Main.dust[num295];
            dust.velocity *= 2f;
            Main.dust[num295].noGravity = true;
        }

        base.Kill(timeLeft);
    }
}