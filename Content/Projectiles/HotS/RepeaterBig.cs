using Terraria;
namespace DoomBubblesMod.Content.Projectiles.HotS;

public class RepeaterBig : Repeater
{
    public override void SetDefaults()
    {
        Projectile.width = 8;
        Projectile.height = 8;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.alpha = 255;
        Projectile.extraUpdates = 2;
        Projectile.scale = 1f;
        Projectile.timeLeft = 600;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ignoreWater = true;
        Projectile.alpha = 69;
    }


    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        modifiers.SourceDamage *= 2;
        modifiers.SourceDamage.Flat += target.lifeMax * .006f;
    }


    public override void OnKill(int timeLeft)
    {
        var num293 = Main.rand.Next(3, 7);
        for (var num294 = 0; num294 < num293; num294++)
        {
            var num295 = Dust.NewDust(Projectile.Center - Projectile.velocity / 2f, 0, 0, DustID.WhiteTorch, 0f, 0f,
                100,
                Color.Blue, 2.1f);
            var dust = Main.dust[num295];
            dust.velocity *= 2f;
            Main.dust[num295].noGravity = true;
        }
    }
}