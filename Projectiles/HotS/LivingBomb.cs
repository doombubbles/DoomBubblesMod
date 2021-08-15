using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;
using SoundType = Terraria.ModLoader.SoundType;

namespace DoomBubblesMod.Projectiles.HotS
{
    public class LivingBomb : CenteredProjectile
    {
        public int ChosenTalent => (int) Math.Round(Projectile.ai[0]);
        public NPC Target => Main.npc[(int) Math.Round(Projectile.ai[1])];

        public int Damage =>
            (int) (ChosenTalent == 2 || ChosenTalent == -1 ? Projectile.damage * 1.35 : Projectile.damage);

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.width = 47;
            Projectile.height = 47;
            Projectile.timeLeft = 300;
            Projectile.scale = 2f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.localNPCHitCooldown = -1;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return Projectile.alpha == 255 ? false : base.CanHitNPC(target);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            if (target.whoAmI != Target.whoAmI && (knockback < .5 || ChosenTalent is 3 or -1))
            {
                target.buffImmune[ModContent.BuffType<Buffs.LivingBomb>()] = false;
                if (!target.HasBuff(ModContent.BuffType<Buffs.LivingBomb>()))
                {
                    target.AddBuff(ModContent.BuffType<Buffs.LivingBomb>(), 150);
                    var proj = Projectile.NewProjectile(new ProjectileSource_ProjectileParent(Projectile), target.Center, new Vector2(0, 0),
                        ModContent.ProjectileType<LivingBomb>(),
                        Damage, ++Projectile.knockBack, Projectile.owner, ChosenTalent, target.whoAmI);
                    Main.projectile[proj].netUpdate = true;
                    SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LivingBombWand2"), target.Center);
                }
                else if (ChosenTalent is 1 or -1 &&
                         target.buffTime[target.FindBuffIndex(ModContent.BuffType<Buffs.LivingBomb>())] < 150)
                {
                    target.buffTime[target.FindBuffIndex(ModContent.BuffType<Buffs.LivingBomb>())] = 152;
                    var proj = Projectile.NewProjectile(new ProjectileSource_ProjectileParent(Projectile), target.Center, new Vector2(0, 0),
                        ModContent.ProjectileType<LivingBomb>(),
                        Damage, ++Projectile.knockBack, Projectile.owner, ChosenTalent, target.whoAmI);
                    Main.projectile[proj].netUpdate = true;
                    SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LivingBombWand2"), target.Center);
                }
            }

            knockback = 0;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void AI()
        {
            if (!Target.active)
            {
                Projectile.Kill();
            }

            Projectile.Center = Target.Center;
            if (Target.HasBuff(ModContent.BuffType<Buffs.LivingBomb>()) && Projectile.timeLeft > 10 && Projectile.timeLeft < 295 &&
                (Target.buffTime[Target.FindBuffIndex(ModContent.BuffType<Buffs.LivingBomb>())] == 1 ||
                 Target.buffTime[Target.FindBuffIndex(ModContent.BuffType<Buffs.LivingBomb>())] == 151))
            {
                Projectile.timeLeft = 10;
                Projectile.alpha = 0;
                SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LivingBoom"), Projectile.Center);
            }

            if (Projectile.timeLeft < 10)
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 2)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                }
            }
        }
    }
}