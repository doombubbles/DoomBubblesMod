using Terraria;

namespace DoomBubblesMod.Projectiles
{
    public class SolarBullet : LunarBullet
    {
        public override int DustType => mod.DustType("Solar229");

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            SolarEffect(ref damage);
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}