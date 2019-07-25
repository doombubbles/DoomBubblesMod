using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using SoundType = Terraria.ModLoader.SoundType;

namespace DoomBubblesMod.Projectiles.HotS
{
    public class LivingFireball : ModProjectile
    {
        public int ChosenTalent => (int) Math.Round(projectile.ai[0]);
        public int Verdant => (int) Math.Round(projectile.ai[1]);
        
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 0;
            projectile.alpha = 0;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.timeLeft = 300;
            projectile.localNPCHitCooldown = -1;
            projectile.magic = true;
            projectile.light = .3f;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (!target.HasBuff(mod.BuffType("LivingBomb")))
            {
                target.buffImmune[mod.BuffType("LivingBomb")] = false;
                target.AddBuff(mod.BuffType("LivingBomb"), 150);
                int proj = Projectile.NewProjectile(target.Center, new Vector2(0, 0), mod.ProjectileType("LivingBomb"),
                    damage * 2, 0, projectile.owner, ChosenTalent, target.whoAmI);
                Main.projectile[proj].netUpdate = true;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LivingBombWand2"), target.Center);
            } else if (ChosenTalent == 1 || ChosenTalent == -1)
            {
                target.buffTime[target.FindBuffIndex(mod.BuffType("LivingBomb"))] = 151;
                int proj = Projectile.NewProjectile(target.Center, new Vector2(0, 0), mod.ProjectileType("LivingBomb"),
                    damage * 2, 0, projectile.owner, ChosenTalent, target.whoAmI);
                Main.projectile[proj].netUpdate = true;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LivingBombWand2"), target.Center);
            }
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void AI()
        {
            if (projectile.timeLeft == 300)
            {
                projectile.penetrate += Verdant;
                projectile.maxPenetrate += Verdant;
            }
            projectile.rotation += .1f;
            Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, 
                    oldVelocity.X * Main.rand.NextFloat(-.8f, -1.2f), oldVelocity.Y * Main.rand.NextFloat(-.8f, -1.2f));
            }
            Main.PlaySound(SoundID.Item10, projectile.oldPosition);
            return base.OnTileCollide(oldVelocity);
        }
    }
}