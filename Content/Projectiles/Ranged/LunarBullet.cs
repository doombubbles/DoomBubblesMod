using System;
using DoomBubblesMod.Content.Dusts;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.WorldBuilding;

namespace DoomBubblesMod.Content.Projectiles.Ranged;

public abstract class LunarBullet : ModProjectile
{
    private readonly float nebulaDistance = 100f;
    public abstract int DustType { get; }

    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.CloneDefaults(ProjectileID.MoonlordBullet);
        Projectile.timeLeft = 200;
        Projectile.penetrate = 1;
    }

    public override void AI()
    {
        Projectile.ai[0] = 0;
        var num117 = Projectile.velocity.Length();
        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= (byte) (num117 * 0.5);
        }

        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }

        var hitbox = Projectile.Hitbox;
        hitbox.Offset((int) Projectile.velocity.X, (int) Projectile.velocity.Y);
        var flag2 = false;
        for (var num118 = 0; num118 < 200; num118++)
        {
            var nPc = Main.npc[num118];
            if (nPc.active &&
                !nPc.dontTakeDamage &&
                nPc.immune[Projectile.owner] == 0 &&
                Projectile.localNPCImmunity[num118] == 0 &&
                nPc.Hitbox.Intersects(hitbox) &&
                !nPc.friendly)
            {
                flag2 = true;
                break;
            }
        }

        if (flag2)
        {
            var num119 = Main.rand.Next(15, 31);
            for (var num120 = 0; num120 < num119; num120++)
            {
                var num121 = Dust.NewDust(Projectile.Center, 0, 0, DustType, 0f, 0f, 100, default, 0.8f);
                Main.dust[num121].velocity *= 1.6f;
                var dust12 = Main.dust[num121];
                dust12.velocity.Y = dust12.velocity.Y - 1f;
                Main.dust[num121].velocity += Projectile.velocity;
                Main.dust[num121].noGravity = true;
            }
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        var spriteEffects = SpriteEffects.None;
        if (Projectile.spriteDirection == -1)
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
        }

        var texture2D3 = TextureAssets.Projectile[Projectile.type].Value;
        var num134 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
        var y12 = num134 * Projectile.frame;
        var rectangle = new Rectangle(0, y12, texture2D3.Width, num134);
        var origin2 = rectangle.Size() / 2f;


        origin2.Y = 2f;


        var num135 = 8;
        var num136 = 2;
        var num137 = 1;
        var value3 = 1f;
        var num138 = 0f;


        num135 = 5;
        num136 = 1;
        value3 = 1f;


        for (var num139 = num137;
             num136 > 0 && num139 < num135 || num136 < 0 && num139 > num135;
             num139 += num136)
        {
            var color26 = lightColor;
            float num142 = num135 - num139;
            if (num136 < 0)
            {
                num142 = num137 - num139;
            }

            color26 *= num142 / (ProjectileID.Sets.TrailCacheLength[Projectile.type] * 1.5f);
            var value4 = Projectile.oldPos[num139];
            var num143 = Projectile.rotation;
            var effects = spriteEffects;

            Main.EntitySpriteDraw(texture2D3,
                value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                rectangle, color26,
                num143 +
                Projectile.rotation *
                num138 *
                (num139 - 1) *
                (0f - spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt()), origin2,
                MathHelper.Lerp(Projectile.scale, value3, num139 / 15f), effects, 0);
        }

        var color28 = Projectile.GetAlpha(lightColor);
        Main.EntitySpriteDraw(texture2D3, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
            rectangle, color28, Projectile.rotation, origin2, Projectile.scale, spriteEffects, 0);

        return false;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        var num576 = Main.rand.Next(2, 5);
        int num534;
        for (var num577 = 0; num577 < num576; num577 = num534 + 1)
        {
            var num578 = Dust.NewDust(Projectile.Center, 0, 0, DustType, 0f, 0f, 100);
            var dust = Main.dust[num578];
            dust.velocity *= 1.6f;
            var dust58 = Main.dust[num578];
            dust58.velocity.Y = dust58.velocity.Y - 1f;
            dust = Main.dust[num578];
            dust.position -= Vector2.One * 4f;
            Main.dust[num578].position = Vector2.Lerp(Main.dust[num578].position, Projectile.Center, 0.5f);
            Main.dust[num578].noGravity = true;
            num534 = num577;
        }

        return base.OnTileCollide(oldVelocity);
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 100) * Projectile.Opacity;
    }

    public void SolarEffect(ref int damage)
    {
        if (!WorldUtils.Find(Projectile.Center.ToTileCoordinates(),
                Searches.Chain(new Searches.Down(12), new Conditions.IsSolid()), out var _))
        {
            damage = (int) (damage * 1.5f);

            for (double theta = 0; theta < Math.PI * 2; theta += 2 * Math.PI / 5)
            {
                var dust = Dust.NewDust(Projectile.Center, 0, 0, DustType<Solar229>(),
                    (float) Math.Cos(theta),
                    (float) Math.Sin(theta));
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 2f;
            }
        }
    }

    public void NebulaEffect()
    {
        var targetI = -1;
        for (var i = 0; i < 200; i++)
        {
            var nPc = Main.npc[i];
            if (nPc != null &&
                nPc.active &&
                nPc.immune[Projectile.owner] == 0 &&
                (!Projectile.usesLocalNPCImmunity || Projectile.localNPCImmunity[i] == 0) &&
                nPc.CanBeChasedBy(Projectile) &&
                nPc.Hitbox.Distance(Projectile.Center) < nebulaDistance)
            {
                targetI = nPc.whoAmI;
                break;
            }
        }

        if (targetI != -1)
        {
            var target = Main.npc[targetI];
            var newPos = new Vector2();

            var dX = Projectile.width / 4;
            var dY = Projectile.height / 4;

            var leftLeft = target.Hitbox.Left < Projectile.Center.X;
            var rightRight = target.Hitbox.Right > Projectile.Center.X;
            var upUp = target.Hitbox.Top < Projectile.Center.Y;
            var downDown = target.Hitbox.Bottom > Projectile.Center.Y;

            if (leftLeft && rightRight)
            {
                newPos.X = Projectile.Center.X;
            }
            else if (leftLeft)
            {
                newPos.X = target.Hitbox.Right - dX;
            }
            else if (rightRight)
            {
                newPos.X = target.Hitbox.Left + dX;
            }

            if (upUp && downDown)
            {
                newPos.Y = Projectile.Center.Y;
            }
            else if (upUp)
            {
                newPos.Y = target.Hitbox.Bottom - dY;
            }
            else if (downDown)
            {
                newPos.Y = target.Hitbox.Top + dY;
            }

            var newDx = Projectile.velocity.Length() * Math.Cos((newPos - Projectile.Center).ToRotation());
            var newDy = Projectile.velocity.Length() * Math.Sin((newPos - Projectile.Center).ToRotation());

            for (double theta = 0; theta < Math.PI * 2; theta += Math.PI / 12)
            {
                var dust1 = Dust.NewDust(
                    Projectile.Center + new Vector2((float) (10 * Math.Cos(theta)), (float) (10 * Math.Sin(theta))),
                    0, 0, DustType<Nebula229>(), (float) Math.Cos(theta + Math.PI),
                    (float) Math.Sin(theta + Math.PI));
                Main.dust[dust1].noGravity = true;

                var dust2 = Dust.NewDust(newPos, 0, 0, DustType<Nebula229>(), (float) Math.Cos(theta),
                    (float) Math.Sin(theta));
                Main.dust[dust2].noGravity = true;
            }

            Projectile.Center = newPos;
            Projectile.velocity = new Vector2((float) newDx, (float) newDy);
        }
    }

    public void VortexEffect(NPC target)
    {
        var pos = Main.player[Projectile.owner].Center +
                  new Vector2(Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-30, 30));
        var v = 7f * (target.Center - pos).ToRotation().ToRotationVector2();

        var proj = Projectile.NewProjectile(new EntitySource_Parent(Projectile), pos, v,
            ProjectileType<VortexBullet2>(),
            Projectile.damage / 2, Projectile.knockBack / 2, Projectile.owner);
        Main.projectile[proj].netUpdate = true;
    }

    public void StardustEffect(NPC target)
    {
        var newPos = Projectile.position;
        newPos += Projectile.oldVelocity / 5f;
        while (target.Hitbox.Contains(newPos.ToPoint()))
        {
            newPos += Projectile.oldVelocity / 5f;
        }

        Projectile.NewProjectile(new EntitySource_Parent(Projectile), newPos,
            Projectile.oldVelocity.RotatedByRandom(Math.PI / 15) / 2,
            ProjectileType<StardustBullet2>(), Projectile.damage / 2, Projectile.knockBack / 2,
            Projectile.owner, 0, target.whoAmI);
        Projectile.NewProjectile(new EntitySource_Parent(Projectile), newPos,
            Projectile.oldVelocity.RotatedByRandom(Math.PI / 15) / 2,
            ProjectileType<StardustBullet2>(), Projectile.damage / 2, Projectile.knockBack / 2,
            Projectile.owner, 0, target.whoAmI);
    }
}