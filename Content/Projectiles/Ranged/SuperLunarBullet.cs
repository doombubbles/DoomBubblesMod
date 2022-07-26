using DoomBubblesMod.Content.Dusts;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public class SuperLunarBullet : LunarBullet
{
    protected override int DustType => Projectile.frame switch
    {
        0 => DustType<Solar229>(),
        1 => DustType<Vortex229>(),
        2 => DustType<Nebula229>(),
        3 => DustType<Stardust229>(),
        _ => DustID.Phantasmal
    };

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

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        VortexEffect(target);
        StardustEffect(target);
        SolarEffect();
    }
}