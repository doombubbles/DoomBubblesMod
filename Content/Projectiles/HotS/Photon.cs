using System;
using DoomBubblesMod.Utils;
using Terraria;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Projectiles.HotS;

public class Photon : CenteredProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 5;
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        ProjectileID.Sets.MinionShot[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 38;
        Projectile.height = 38;
        Projectile.scale = .6f;
        Projectile.penetrate = -1;
        Projectile.friendly = true;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 300;
        Projectile.aiStyle = -1;
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (target.whoAmI == (int) Projectile.ai[1])
        {
            Projectile.penetrate = 1;
            SoundEngine.PlaySound(Mod.Sound("PhotonHit"), Projectile.Center);
        }

        base.ModifyHitNPC(target, ref modifiers);
    }

    public override void AI()
    {
        Projectile.frame++;
        if (Projectile.frame > 4)
        {
            Projectile.frame = 0;
        }

        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }

        Projectile.localAI[0] = Projectile.velocity.Length() * 1.02f;
        Homing();
    }


    public void Homing()
    {
        var target = Main.npc[(int) Projectile.ai[1]];
        if (!target.active)
        {
            Projectile.Kill();
        }

        var x = Projectile.Center.X;
        var y = Projectile.Center.Y - 6;
        var theta = Math.Atan2(target.Center.Y - y, target.Center.X - x);
        var dX = Projectile.localAI[0] * Math.Cos(theta);
        var dY = Projectile.localAI[0] * Math.Sin(theta);
        Projectile.velocity = new Vector2((float) dX, (float) dY);
    }
}