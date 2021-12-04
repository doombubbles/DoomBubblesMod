using DoomBubblesMod.Content.Dusts;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class SuperLunarBullet : LunarBullet
{
    public override int DustType
    {
        get
        {
            if (Projectile.frame == 0)
            {
                return ModContent.DustType<Solar229>();
            }

            if (Projectile.frame == 1)
            {
                return ModContent.DustType<Vortex229>();
            }

            if (Projectile.frame == 2)
            {
                return ModContent.DustType<Nebula229>();
            }

            if (Projectile.frame == 3)
            {
                return ModContent.DustType<Stardust229>();
            }

            return 229;
        }
    }

    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 4;
    }

    public override void AI()
    {
        if (Projectile.alpha == 255)
        {
            Projectile.frame = Main.rand.Next(0, 3);
        }

        base.AI();

        Projectile.frameCounter++;
        if (Projectile.frameCounter > 10)
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;
            if (Projectile.frame >= 4)
            {
                Projectile.frame = 0;
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