using System;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Utils;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Projectiles.HotS;

public class FlameStrike : CenteredProjectile
{
    public static readonly int Delay = 60;
    public static readonly int Visible = 10;

    public float Size => 75f + Verdant * 37.5f;
    public int ChosenTalent => (int) Math.Round(Projectile.ai[0]);
    public int Verdant => (int) Math.Round(Projectile.ai[1]);

    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 5;
    }

    public override void SetDefaults()
    {
        Projectile.width = 89;
        Projectile.height = 89;
        Projectile.timeLeft = Delay + Visible;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.light = .5f;
        Projectile.aiStyle = -1;
        Projectile.ignoreWater = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.scale = 2f;
        Projectile.alpha = 255;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 15;
    }

    public override bool? CanHitNPC(NPC target)
    {
        if (Projectile.alpha == 255)
        {
            return false;
        }

        if (target.Hitbox.Distance(Projectile.Center) > Size)
        {
            return false;
        }

        return base.CanHitNPC(target);
    }


    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
        ref int hitDirection)
    {
        if ((ChosenTalent == 2 || ChosenTalent == -1) && Projectile.localAI[1] < .5)
        {
            var ai = Main.player[Projectile.owner].gravControl2 ? 1 : 0;
            if (Main.player[Projectile.owner].gravControl2)
            {
                target.AddBuff(BuffType<Buffs.LivingBomb>(), 152);
                var proj = Projectile.NewProjectile(new EntitySource_Parent(Projectile), target.Center,
                    new Vector2(0, 0),
                    ProjectileType<LivingBomb>(),
                    (int) (damage / 115f * 160f), 0, Projectile.owner, ai, target.whoAmI);
                Main.projectile[proj].netUpdate = true;
                SoundEngine.PlaySound(Mod.Sound("LivingBombWand2"), target.Center);
            }
            else if (!target.HasBuff(BuffType<Buffs.LivingBomb>()))
            {
                target.AddBuff(BuffType<Buffs.LivingBomb>(), 150);
                var proj = Projectile.NewProjectile(new EntitySource_Parent(Projectile), target.Center,
                    new Vector2(0, 0),
                    ProjectileType<LivingBomb>(),
                    (int) (damage / 115f * 160f), 0, Projectile.owner, ai, target.whoAmI);
                Main.projectile[proj].netUpdate = true;

                Projectile.localAI[1] += 2 + ai * 2;
                SoundEngine.PlaySound(Mod.Sound("LivingBombWand2"), target.Center);
            }
        }

        if (Verdant > 0)
        {
            damage += target.defDefense * Verdant;
        }

        if ((ChosenTalent == 1 || ChosenTalent == -1) &&
            Main.player[Projectile.owner].GetModPlayer<HotSPlayer>().convection < 100)
        {
            Main.player[Projectile.owner].GetModPlayer<HotSPlayer>().convection++;
            if (!Main.player[Projectile.owner].HasBuff<Convection>())
            {
                Main.player[Projectile.owner].AddBuff(BuffType<Convection>(), 10);
            }
        }

        base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        base.OnHitNPC(target, damage, knockback, crit);
        target.immune[Projectile.owner] = 0;
    }

    public override void AI()
    {
        if (Projectile.timeLeft == Delay + Visible)
        {
            if (Verdant > 0)
            {
                var center = Projectile.Center;
                Projectile.width = 178 + Verdant * 89;
                Projectile.height = 178 + Verdant * 89;
                Projectile.scale = 2f + Verdant;
                Projectile.Center = center;
                SoundEngine.PlaySound(Mod.Sound("Flame2"), Projectile.position);
            }
            else
            {
                SoundEngine.PlaySound(Mod.Sound("Flame"), Projectile.position);
            }
        }
        else if (Projectile.timeLeft == Visible)
        {
            Projectile.alpha = 0;
            if (Verdant > 0)
            {
                SoundEngine.PlaySound(Mod.Sound("Strike2"), Projectile.position);
            }
            else
            {
                SoundEngine.PlaySound(Mod.Sound("Strike"), Projectile.position);
            }
        }
        else if (Projectile.timeLeft < Visible)
        {
            Projectile.alpha = 0;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }
        }
        else
        {
            for (var i = 0; i < 12 * (1 - (Projectile.timeLeft - Visible) / (float) Delay); i++)
            {
                foreach (var z in new[] {0, 120, 240})
                {
                    var x = Projectile.Center.X + Size * Math.Cos((z + 10 * i) * Math.PI / 180);
                    var y = Projectile.Center.Y + Size * Math.Sin((z + 10 * i) * Math.PI / 180);
                    var d = Dust.NewDust(new Vector2((float) x, (float) y), 0, 0, DustID.CursedTorch);
                    Main.dust[d].noGravity = true;
                }
            }
        }

        if (Projectile.timeLeft == 1 && Projectile.localAI[0] < 1 && (ChosenTalent == 3 || ChosenTalent == -1))
        {
            Projectile.timeLeft = Delay + Visible + 1;
            Projectile.alpha = 255;
            Projectile.localAI[0]++;
            Projectile.frame = 0;
            Projectile.localAI[1] = 0;
        }
    }
}