using System;
using Terraria;

namespace DoomBubblesMod.Content.Projectiles;

public class Damage : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 1;
        Projectile.height = 1;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.alpha = 255;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 2;
        Projectile.penetrate = 1;
    }

    public override bool? CanHitNPC(NPC target)
    {
        return target.whoAmI == (int) Math.Round(Projectile.ai[0]);
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if ((int) Math.Round(Projectile.ai[1]) == 1)
        {
            modifiers.SetCrit();
        }
        base.ModifyHitNPC(target, ref modifiers);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(target, hit, damageDone);
        Projectile.Kill();
    }
}