using System;
using Terraria.Audio;
using Terraria.DataStructures;

namespace DoomBubblesMod.Content.Projectiles.HotS;

public class LivingFireball : ModProjectile
{
    public int ChosenTalent => (int) Math.Round(Projectile.ai[0]);
    public int Verdant => (int) Math.Round(Projectile.ai[1]);

    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.tileCollide = true;
        Projectile.penetrate = 1;
        Projectile.ignoreWater = true;
        Projectile.extraUpdates = 0;
        Projectile.alpha = 0;
        Projectile.extraUpdates = 1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.timeLeft = 300;
        Projectile.localNPCHitCooldown = -1;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.light = .3f;
    }

    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
        ref int hitDirection)
    {
        if (!target.HasBuff(ModContent.BuffType<Buffs.LivingBomb>()))
        {
            target.buffImmune[ModContent.BuffType<Buffs.LivingBomb>()] = false;
            target.AddBuff(ModContent.BuffType<Buffs.LivingBomb>(), 150);
            var proj = Projectile.NewProjectile(new ProjectileSource_ProjectileParent(Projectile), target.Center,
                new Vector2(0, 0), ModContent.ProjectileType<LivingBomb>(),
                damage * 2, 0, Projectile.owner, ChosenTalent, target.whoAmI);
            Main.projectile[proj].netUpdate = true;
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/LivingBombWand2"), target.Center);
        }
        else if (ChosenTalent == 1 || ChosenTalent == -1)
        {
            target.buffTime[target.FindBuffIndex(ModContent.BuffType<Buffs.LivingBomb>())] = 151;
            var proj = Projectile.NewProjectile(new ProjectileSource_ProjectileParent(Projectile), target.Center,
                new Vector2(0, 0), ModContent.ProjectileType<LivingBomb>(),
                damage * 2, 0, Projectile.owner, ChosenTalent, target.whoAmI);
            Main.projectile[proj].netUpdate = true;
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/LivingBombWand2"), target.Center);
        }

        base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
    }

    public override void AI()
    {
        if (Projectile.timeLeft == 300)
        {
            Projectile.penetrate += Verdant;
            Projectile.maxPenetrate += Verdant;
        }

        Projectile.rotation += .1f;
        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FlameBurst);
    }


    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        for (var i = 0; i < 5; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FlameBurst,
                oldVelocity.X * Main.rand.NextFloat(-.8f, -1.2f), oldVelocity.Y * Main.rand.NextFloat(-.8f, -1.2f));
        }

        SoundEngine.PlaySound(SoundID.Item10, Projectile.oldPosition);
        return base.OnTileCollide(oldVelocity);
    }
}