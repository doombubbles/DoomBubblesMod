using System;
using Terraria.GameContent;

namespace DoomBubblesMod.Content.Projectiles.HotS;

public abstract class HotsProjectile : ModProjectile
{
    protected virtual int ChosenTalent => (int) Math.Round(Projectile.ai[0]);

    protected virtual bool Centered => false;
    
    public override bool PreDraw(ref Color lightColor)
    {
        if (Centered)
        {
            var texture2D = TextureAssets.Projectile[Projectile.type].Value;
            var height = texture2D.Height / Main.projFrames[Projectile.type];
            var y = height * Projectile.frame;
            var pos = (Projectile.position +
                       new Vector2(Projectile.width, Projectile.height) / 2f +
                       Vector2.UnitY * Projectile.gfxOffY -
                       Main.screenPosition).Floor();
            Main.EntitySpriteDraw(texture2D, pos, new Rectangle(0, y, texture2D.Width, height),
                Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2(texture2D.Width / 2f, height / 2f),
                Projectile.scale, SpriteEffects.None, 0);
            return false;
        }

        return base.PreDraw(ref lightColor);
    }
}

public abstract class HotsProjectile2 : HotsProjectile
{
    protected override int ChosenTalent => (int) Math.Round(Projectile.ai[1]);
}

public abstract class KaelThasProjectile : HotsProjectile
{
    protected int Verdant => (int) Math.Round(Projectile.ai[1]);
}