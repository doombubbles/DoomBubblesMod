using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public abstract class HappyProjectile : ModProjectile
    {
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D = Main.projectileTexture[projectile.type];
            int height = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y = height * projectile.frame;
            Vector2 pos = (projectile.position + new Vector2(projectile.width, projectile.height) / 2f +
                           Vector2.UnitY * projectile.gfxOffY - Main.screenPosition).Floor();
            spriteBatch.Draw(Main.projectileTexture[projectile.type], pos, new Rectangle(0, y, texture2D.Width, height), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture2D.Width / 2f, (float) height / 2f), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}