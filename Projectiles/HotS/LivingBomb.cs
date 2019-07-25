using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.HotS
{
    public class LivingBomb : HappyProjectile
    {
        public int ChosenTalent => (int) Math.Round(projectile.ai[0]);
        public NPC Target => Main.npc[(int) Math.Round(projectile.ai[1])];

        public int Damage => (int) (ChosenTalent == 2 || ChosenTalent == -1 ? projectile.damage * 1.35 : projectile.damage);

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.width = 47;
            projectile.height = 47;
            projectile.timeLeft = 300;
            projectile.scale = 2f;
            projectile.usesLocalNPCImmunity = true;
            projectile.magic = true;
            projectile.localNPCHitCooldown = -1;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.alpha == 255)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.whoAmI != Target.whoAmI && (knockback <.5 || ChosenTalent == 3 || ChosenTalent == -1))
            {
                target.buffImmune[mod.BuffType("LivingBomb")] = false;
                if (!target.HasBuff(mod.BuffType("LivingBomb")))
                {
                    target.AddBuff(mod.BuffType("LivingBomb"), 150);
                    int proj = Projectile.NewProjectile(target.Center, new Vector2(0, 0), mod.ProjectileType("LivingBomb"),
                        Damage, ++projectile.knockBack, projectile.owner, ChosenTalent, target.whoAmI);
                    Main.projectile[proj].netUpdate = true;
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LivingBombWand2"), target.Center);
                } else if ((ChosenTalent == 1 || ChosenTalent == -1) && target.buffTime[target.FindBuffIndex(mod.BuffType("LivingBomb"))] < 150)
                {
                    target.buffTime[target.FindBuffIndex(mod.BuffType("LivingBomb"))] = 152;
                    int proj = Projectile.NewProjectile(target.Center, new Vector2(0, 0), mod.ProjectileType("LivingBomb"),
                        Damage, ++projectile.knockBack, projectile.owner, ChosenTalent, target.whoAmI);
                    Main.projectile[proj].netUpdate = true;
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LivingBombWand2"), target.Center);
                }
            }

            knockback = 0;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void AI()
        {
            if (!Target.active)
            {
                projectile.Kill();
            }
            projectile.Center = Target.Center;
            if (Target.HasBuff(mod.BuffType("LivingBomb")) && projectile.timeLeft > 10 && projectile.timeLeft < 295 &&
                (Target.buffTime[Target.FindBuffIndex(mod.BuffType("LivingBomb"))] == 1 ||
                 Target.buffTime[Target.FindBuffIndex(mod.BuffType("LivingBomb"))] == 151))
            {
                projectile.timeLeft = 10;
                projectile.alpha = 0;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LivingBoom"), projectile.Center);
            }

            if (projectile.timeLeft < 10)
            {
                projectile.frameCounter++;
                if (projectile.frameCounter >= 2)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
            }
        }
    }
}