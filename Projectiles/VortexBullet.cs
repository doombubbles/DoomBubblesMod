using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace DoomBubblesMod.Projectiles
{
    public class VortexBullet : LunarBullet
    {
        public override int DustType => mod.DustType("Vortex229");

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            Vector2 pos = Main.player[projectile.owner].Center +
                          new Vector2(Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-30, 30));
            Vector2 v = 7f * (target.Center - pos).ToRotation().ToRotationVector2();
            
            int proj = Projectile.NewProjectile(pos, v, mod.ProjectileType("VortexBullet2"), 
                projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
            Main.projectile[proj].netUpdate = true;
        }
    }
}