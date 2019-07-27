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
            VortexEffect(target);
        }
    }
}