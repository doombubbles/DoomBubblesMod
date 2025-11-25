using DoomBubblesMod.Utils;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Projectiles.HotS;

public class LivingFireball : KaelThasProjectile
{
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

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (!target.HasBuff(BuffType<Buffs.LivingBomb>()))
        {
            target.buffImmune[BuffType<Buffs.LivingBomb>()] = false;
            target.AddBuff(BuffType<Buffs.LivingBomb>(), 150);
            var proj = Projectile.NewProjectile(new EntitySource_Parent(Projectile), target.Center,
                new Vector2(0, 0), ProjectileType<LivingBomb>(),
                Projectile.damage * 2, 0, Projectile.owner, ChosenTalent, target.whoAmI);
            Main.projectile[proj].netUpdate = true;
            SoundEngine.PlaySound(Mod.Sound("LivingBombWand2"), target.Center);
        }
        else if (ChosenTalent is 1 or -1)
        {
            target.buffTime[target.FindBuffIndex(BuffType<Buffs.LivingBomb>())] = 151;
            var proj = Projectile.NewProjectile(new EntitySource_Parent(Projectile), target.Center,
                new Vector2(0, 0), ProjectileType<LivingBomb>(),
                Projectile.damage * 2, 0, Projectile.owner, ChosenTalent, target.whoAmI);
            Main.projectile[proj].netUpdate = true;
            SoundEngine.PlaySound(Mod.Sound("LivingBombWand2"), target.Center);
        }

        base.ModifyHitNPC(target, ref modifiers);
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