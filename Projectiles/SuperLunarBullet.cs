using Terraria;

namespace DoomBubblesMod.Projectiles
{
    public class SuperLunarBullet : LunarBullet
    {
        public override int DustType
        {
            get
            {
                if (projectile.frame == 0)
                {
                    return mod.DustType("Solar229");
                }

                if (projectile.frame == 1)
                {
                    return mod.DustType("Vortex229");
                }

                if (projectile.frame == 2)
                {
                    return mod.DustType("Nebula229");
                }

                if (projectile.frame == 3)
                {
                    return mod.DustType("Stardust229");
                }

                return 229;
            }
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void AI()
        {
            if (projectile.alpha == 255)
            {
                projectile.frame = Main.rand.Next(0, 3);
            }

            base.AI();

            projectile.frameCounter++;
            if (projectile.frameCounter > 10)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }

            NebulaEffect();
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            SolarEffect(ref damage);
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            VortexEffect(target);
            StardustEffect(target);
        }
    }
}