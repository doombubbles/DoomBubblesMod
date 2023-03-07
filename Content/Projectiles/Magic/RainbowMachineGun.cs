using System;
using ElementalDamage.Common.Types;
using ElementalDamage.Content.DamageClasses;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent;

namespace DoomBubblesMod.Content.Projectiles.Magic;

public class RainbowMachineGun : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 6;
    }

    public override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 22;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.hide = true;
        Projectile.DamageType = ElementalDamageClass.Get<MagicDamageClass, Holy>();
        Projectile.ignoreWater = true;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        var spriteEffects = SpriteEffects.None;

        var texture2D14 = TextureAssets.Projectile[Projectile.type].Value;
        var num192 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
        var y16 = num192 * Projectile.frame;
        var vector27 = (Projectile.position +
                        new Vector2(Projectile.width, Projectile.height) / 2f +
                        Vector2.UnitY * Projectile.gfxOffY -
                        Main.screenPosition).Floor();
        var scale5 = 1f;

        Main.EntitySpriteDraw(texture2D14, vector27,
            new Rectangle(0, y16, texture2D14.Width, num192), Projectile.GetAlpha(lightColor),
            Projectile.rotation, new Vector2(texture2D14.Width / 2f, num192 / 2f), Projectile.scale,
            spriteEffects, 0);


        Main.EntitySpriteDraw(Request<Texture2D>(Texture + "_Glow", AssetRequestMode.ImmediateLoad).Value,
            vector27,
            new Rectangle(0, y16, texture2D14.Width, num192),
            new Color(255, 255, 255, 0) * scale5, Projectile.rotation,
            new Vector2(texture2D14.Width / 2f, num192 / 2f), Projectile.scale, spriteEffects,
            0);

        return false;
    }

    public override bool? CanDamage()
    {
        return false;
    }

    public override void AI()
    {
        var player = Main.player[Projectile.owner];
        var num = (float) Math.PI / 2f;
        var vector = player.RotatedRelativePoint(player.MountedCenter);
        Projectile.ai[0] += 1f;
        var num2 = 0;
        var num3 = 24;
        var num4 = 6;
        if (Projectile.ai[0] >= 40f)
        {
            num2++;
        }

        if (Projectile.ai[0] >= 80f)
        {
            num2++;
        }

        if (Projectile.ai[0] >= 120f)
        {
            num2++;
        }

        if (Projectile.ai[0] >= 180f)
        {
            num3 = 22;
        }

        Projectile.ai[1] += 1f;
        var flag = false;
        if (Projectile.ai[1] >= num3 - num4 * num2)
        {
            Projectile.ai[1] = 0f;
            flag = true;
        }

        Projectile.frameCounter += 1 + num2;
        if (Projectile.frameCounter >= 4)
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;
            if (Projectile.frame >= 6)
            {
                Projectile.frame = 0;
            }
        }

        if (Projectile.soundDelay <= 0)
        {
            Projectile.soundDelay = num3 - num4 * num2;
            if (Math.Abs(Projectile.ai[0] - 1f) > .0001f)
            {
                SoundEngine.PlaySound(SoundID.Item91, Projectile.position);
            }
        }


        if (Projectile.ai[1] == 1f && Projectile.ai[0] != 1f)
        {
            var spinningpoint = Vector2.UnitX * 24f;
            spinningpoint = spinningpoint.RotatedBy(Projectile.rotation - (float) Math.PI / 2f);
            var value = Projectile.Center + spinningpoint;
            for (var i = 0; i < 2; i++)
            {
                var num5 = Dust.NewDust(value - Vector2.One * 8f, 16, 16, DustID.RainbowMk2, Projectile.velocity.X / 2f,
                    Projectile.velocity.Y / 2f, 100, RainbowColors[Main.rand.Next(0, 6)]);
                Main.dust[num5].velocity *= 0.66f;
                Main.dust[num5].noGravity = true;
                Main.dust[num5].scale = 1.4f;
            }
        }

        if (flag && Main.myPlayer == Projectile.owner)
        {
            if (player.channel &&
                player.CheckMana(player.inventory[player.selectedItem], -1, true) &&
                !player.noItems &&
                !player.CCed)
            {
                var scaleFactor = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                var vector2 = vector;
                var value2 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - vector2;
                if (player.gravDir == -1f)
                {
                    value2.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - vector2.Y;
                }

                var vector3 = Vector2.Normalize(value2);
                if (float.IsNaN(vector3.X) || float.IsNaN(vector3.Y))
                {
                    vector3 = -Vector2.UnitY;
                }

                vector3 *= scaleFactor;
                if (vector3.X != Projectile.velocity.X || vector3.Y != Projectile.velocity.Y)
                {
                    Projectile.netUpdate = true;
                }

                Projectile.velocity = vector3;
                var scaleFactor2 = 14f;
                var num7 = 7;
                for (var j = 0; j < 2; j++)
                {
                    vector2 = Projectile.Center +
                              new Vector2(Main.rand.Next(-num7, num7 + 1),
                                  Main.rand.Next(-num7, num7 + 1));
                    var spinningpoint2 = Vector2.Normalize(Projectile.velocity) * scaleFactor2;
                    spinningpoint2 =
                        spinningpoint2.RotatedBy(
                            Main.rand.NextDouble() * 0.19634954631328583 - 0.098174773156642914);
                    if (float.IsNaN(spinningpoint2.X) || float.IsNaN(spinningpoint2.Y))
                    {
                        spinningpoint2 = -Vector2.UnitY;
                    }

                    var proj = Projectile.NewProjectile(new EntitySource_Parent(Projectile), vector2.X,
                        vector2.Y, spinningpoint2.X, spinningpoint2.Y,
                        ProjectileType<Rainbow>(), Projectile.damage, Projectile.knockBack,
                        Projectile.owner);
                    Main.projectile[proj].netUpdate = true;
                }
            }
            else
            {
                Projectile.Kill();
            }
        }

        Projectile.position = player.RotatedRelativePoint(player.MountedCenter) - Projectile.Size / 2f;
        Projectile.rotation = Projectile.velocity.ToRotation() + num;
        Projectile.spriteDirection = Projectile.direction;
        Projectile.timeLeft = 2;
        player.ChangeDir(Projectile.direction);
        player.heldProj = Projectile.whoAmI;
        player.itemTime = 2;
        player.itemAnimation = 2;
        player.itemRotation = (float) Math.Atan2(Projectile.velocity.Y * Projectile.direction,
            Projectile.velocity.X * Projectile.direction);
        /*for (int num46 = 0; num46 < 2; num46++)
        {
            Dust obj = Main.dust[Dust.NewDust(Projectile.position + Projectile.velocity * 2f, Projectile.width, Projectile.height, 6, 0f, 0f, 100, Color.Transparent, 2f)];
            obj.noGravity = true;
            obj.velocity *= 2f;
            obj.velocity += Projectile.localAI[0].ToRotationVector2();
            obj.fadeIn = 1.5f;
        }*/

        /*
        for (int num48 = 0; (float) num48 < num47; num48++)
        {
            if (Main.rand.Next(4) == 0)
            {
                Vector2 position = Projectile.position + Projectile.velocity +
                                   Projectile.velocity * ((float) num48 / num47);
                Dust obj2 = Main.dust[
                    Dust.NewDust(position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, Color.Transparent)];
                obj2.noGravity = true;
                obj2.fadeIn = 0.5f;
                obj2.velocity += Projectile.localAI[0].ToRotationVector2();
                obj2.noLight = true;
            }
        }
        */
    }

    public override void PostAI()
    {
        var player = Main.player[Projectile.owner];
        var item = player.inventory[player.selectedItem];
        var time = (int) (item.useTime / CombinedHooks.TotalUseTimeMultiplier(player, item));
        Projectile.ai[0] += 20f / time - 1;
        Projectile.ai[1] += 20f / time - 1;
    }
}