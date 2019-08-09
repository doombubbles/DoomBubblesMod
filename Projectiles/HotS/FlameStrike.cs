using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using SoundType = Terraria.ModLoader.SoundType;

namespace DoomBubblesMod.Projectiles.HotS
{
    public class FlameStrike : HappyProjectile
    {
        public static readonly int Delay = 60;
        public static readonly int Visible = 10;

        public float Size => 75f + Verdant * 37.5f;
        public int ChosenTalent => (int) Math.Round(projectile.ai[0]);
        public int Verdant => (int) Math.Round(projectile.ai[1]);

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.width = 89;
            projectile.height = 89;
            projectile.timeLeft = Delay + Visible;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.light = .5f;
            projectile.aiStyle = -1;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.scale = 2f;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 15;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.alpha == 255)
            {
                return false;
            }

            if (target.Hitbox.Distance(projectile.Center) > Size)
            {
                return false;
            }
            
            return base.CanHitNPC(target);
        }


        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if ((ChosenTalent == 2 || ChosenTalent == -1) && projectile.localAI[1] < .5)
            {
                int ai = Main.player[projectile.owner].gravControl2 ? 1 : 0;
                if (Main.player[projectile.owner].gravControl2)
                {
                    target.AddBuff(mod.BuffType("LivingBomb"), 152);
                    int proj = Projectile.NewProjectile(target.Center, new Vector2(0, 0), mod.ProjectileType("LivingBomb"),
                        (int) (damage / 115f * 160f), 0, projectile.owner, ai, target.whoAmI);
                    Main.projectile[proj].netUpdate = true;
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LivingBombWand2"), target.Center);
                } else if (!target.HasBuff(mod.BuffType("LivingBomb")))
                {
                    target.AddBuff(mod.BuffType("LivingBomb"), 150);
                    int proj = Projectile.NewProjectile(target.Center, new Vector2(0, 0), mod.ProjectileType("LivingBomb"),
                        (int) (damage / 115f * 160f), 0, projectile.owner, ai, target.whoAmI);
                    Main.projectile[proj].netUpdate = true;
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LivingBombWand2"), target.Center);

                    projectile.localAI[1] += 2 + ai * 2;
                }
            }

            if (Verdant > 0)
            {
                damage += target.defDefense * Verdant;
            }

            if ((ChosenTalent == 1 || ChosenTalent == -1) && Main.player[projectile.owner].GetModPlayer<HotSPlayer>().convection < 100)
            {
                Main.player[projectile.owner].GetModPlayer<HotSPlayer>().convection++;
                if (!Main.player[projectile.owner].HasBuff(mod.BuffType("Convection")))
                {
                    Main.player[projectile.owner].AddBuff(mod.BuffType("Convection"), 10);
                }
            }
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.immune[projectile.owner] = 0;
        }

        public override void AI()
        {
            if (projectile.timeLeft == Delay + Visible)
            {
                if (Verdant > 0)
                {
                    var center = projectile.Center;
                    projectile.width = 178 + Verdant * 89;
                    projectile.height = 178 + Verdant * 89;
                    projectile.scale = 2f + Verdant;
                    projectile.Center = center;
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Flame2"), projectile.position);
                }
                else
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Flame"), projectile.position);
                }
            } else if (projectile.timeLeft == Visible)
            {
                projectile.alpha = 0;
                if (Verdant > 0)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Strike2"), projectile.position);
                }
                else
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Strike"), projectile.position);
                }
            } else if (projectile.timeLeft < Visible)
            {
                projectile.alpha = 0;
                projectile.frameCounter++;
                if (projectile.frameCounter >= 2)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
            }
            else
            {
                for (int i = 0; i < 12 * (1 -(projectile.timeLeft - Visible) / (float) Delay); i++)
                {
                    foreach (var z in new []{0, 120, 240})
                    {
                        double x = projectile.Center.X + Size * Math.Cos((z + 10 * i) * Math.PI / 180);
                        double y = projectile.Center.Y + Size * Math.Sin((z + 10 * i) * Math.PI / 180);
                        int d = Dust.NewDust(new Vector2((float) x, (float) y), 0, 0, 75);
                        Main.dust[d].noGravity = true;
                    }
                }
            }

            if (projectile.timeLeft == 1 && projectile.localAI[0] < 1 && (ChosenTalent == 3 || ChosenTalent == -1))
            {
                projectile.timeLeft = Delay + Visible + 1;
                projectile.alpha = 255;
                projectile.localAI[0]++;
                projectile.frame = 0;
                projectile.localAI[1] = 0;
            } 
            
        }
        
    }
}