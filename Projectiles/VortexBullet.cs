using DoomBubblesMod.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class VortexBullet : LunarBullet
    {
        public override int DustType => ModContent.DustType<Vortex229>();

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            VortexEffect(target);
        }
    }
}