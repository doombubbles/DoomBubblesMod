using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public abstract class CenteredProjectile : ModProjectile
    {
        public override bool PreDraw(ref Color lightColor)
        {
            var texture2D = TextureAssets.Projectile[Projectile.type].Value;
            var height = texture2D.Height / Main.projFrames[Projectile.type];
            var y = height * Projectile.frame;
            var pos = (Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f +
                Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            Main.EntitySpriteDraw(texture2D, pos, new Rectangle(0, y, texture2D.Width, height),
                Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2(texture2D.Width / 2f, height / 2f),
                Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}