using System;
using System.Collections.Generic;
using System.Linq;
using DoomBubblesMod.Items.HotS;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod
{
    public class LoLPlayer : ModPlayer
    {
        public RunePath primaryPath;
        //public RunePath secondaryPath;

        public int keystone;
        
        //public int[] primarySlots;
        //public int[] secondarySlots;

        private LoLPlayer loLPlayer => player.GetModPlayer<LoLPlayer>();

        #region keystones
        
        public bool PressTheAttack => primaryPath == RunePath.Precision && keystone == 1;
        public bool LethalTempo => primaryPath == RunePath.Precision && keystone == 2;
        public bool FleetFootwork => primaryPath == RunePath.Precision && keystone == 3;
        public bool Conquerer => primaryPath == RunePath.Precision && keystone == 4;
        
        public bool Electrocute => primaryPath == RunePath.Domination && keystone == 1;
        public bool Predator => primaryPath == RunePath.Domination && keystone == 2;
        public bool DarkHarvest => primaryPath == RunePath.Domination && keystone == 3;
        public bool HailOfBlades => primaryPath == RunePath.Domination && keystone == 4;
        
        public bool SummonAery => primaryPath == RunePath.Sorcery && keystone == 1;
        public bool ArcaneComet => primaryPath == RunePath.Sorcery && keystone == 2;
        public bool PhaseRush => primaryPath == RunePath.Sorcery && keystone == 3;
        
        public bool GraspOfTheUndying => primaryPath == RunePath.Resolve && keystone == 1;
        public bool Aftershock => primaryPath == RunePath.Resolve && keystone == 2;
        public bool Guardian => primaryPath == RunePath.Resolve && keystone == 3;
        
        public bool GlacialAugment => primaryPath == RunePath.Inspiration && keystone == 1;
        public bool UnsealedSpellbook => primaryPath == RunePath.Inspiration && keystone == 2;
        public bool Kleptomancy => primaryPath == RunePath.Inspiration && keystone == 3;

        #endregion

        //public static Dictionary<RunePath, List<Rune>[]> RUNES;

        public int keystoneCooldown;
        public int conquererStacks;
        public int energized;
        public int tripleStacks;
        public int lastHit;
        public int darkHarvestSouls;
        public int aery;
        public double grasp;
        public Dictionary<int, int> spellbook;
        
        public override void ResetEffects()
        {
            primaryPath = RunePath.None;
            //secondaryPath = RunePath.None;
            keystone = 0;
            //primarySlots = new int[3];
            //secondarySlots = new int[2];
            if (spellbook == null) spellbook = new Dictionary<int, int>();
            foreach (var key in spellbook.Keys.ToList())
            {
                spellbook[key] = Math.Max(0, spellbook[key] - 1);
            }
        
            if (!player.HasBuff(mod.BuffType("Conquerer"))) conquererStacks = 0;
            if (energized > 100) energized = 100;
            keystoneCooldown -= 1;
            if (keystoneCooldown < 0) keystoneCooldown = 0;
            tripleStacks -= 1;
            if (tripleStacks < 0) tripleStacks = 0;
            if (tripleStacks == 240) tripleStacks = 0;
            if (tripleStacks == 0) lastHit = 0;
        }

        public override void UpdateDead()
        {
            aery = 0;
            darkHarvestSouls = 0;
            grasp = 0;
        }

        public override void PostUpdateEquips()
        {
            player.allDamageMult += .01f * loLPlayer.conquererStacks;
            if (energized == 100 && FleetFootwork) player.AddBuff(mod.BuffType("Energized"), 2);
            if (keystoneCooldown == 0 && Electrocute) player.AddBuff(mod.BuffType("Electrocute"), 2);

            energized += Main.rand.NextDouble() < (player.velocity.Length() / 50f) ? 1 : 0;

            if (PressTheAttack && lastHit != 0)
            {
                NPC target = Main.npc[lastHit - 1];
                if (tripleStacks > 0 && target.active)
                {
                    Vector2 dir = new Vector2(3, (float) Math.Sqrt(3));
                    Dust.NewDustPerfect(target.Center + dir * 3, 57, target.velocity / 2 + dir / 2);
                }

                if (tripleStacks > 240 && target.active)
                {
                    Vector2 dir = new Vector2(-3, (float) Math.Sqrt(3));
                    Dust.NewDustPerfect(target.Center + dir * 3, 57, target.velocity / 2 + dir / 2);
                }
            }
            
            if (DarkHarvest && keystoneCooldown == 0)
            {
                player.AddBuff(mod.BuffType("DarkHarvest"), 2);
            }
            
            if (keystoneCooldown == 1 && HailOfBlades)
            {
                bool dewit = false;
                foreach (var npc in Main.npc)
                {
                    if (npc.active && npc.boss)
                    {
                        dewit = true;
                        break;
                    }
                }
                if (dewit) keystoneCooldown = 4 * 60;
            }

            if (ArcaneComet && keystoneCooldown == 0)
            {
                int i = (int) (Main.time % 360);
                Dust d = Dust.NewDustPerfect(player.Center + 40 * new Vector2((float) Math.Cos(Math.PI * i / 180.0), .9f * (float) Math.Sin(Math.PI * i / 180.0)), mod.DustType("ManapireDust"), player.velocity / 2);
                d.noGravity = true;
            }
            
            if (GlacialAugment && keystoneCooldown == 0)
            {
                int i = (int) (Main.time % 360);
                Dust d = Dust.NewDustPerfect(player.Center + 40 * new Vector2((float) Math.Cos(Math.PI * i / 180.0), .9f * (float) Math.Sin(Math.PI * i / 180.0)), mod.DustType("LightBlue182"), player.velocity / 2);
                d.noGravity = true;
            }
            
            if (GraspOfTheUndying && keystoneCooldown == 0)
            {
                int i = (int) (Main.time % 360);
                Dust d = Dust.NewDustPerfect(player.Center + 40 * new Vector2((float) Math.Cos(Math.PI * i / 180.0), .9f * (float) Math.Sin(Math.PI * i / 180.0)), mod.DustType("Green182"), player.velocity / 2);
                d.noGravity = true;
            }

            if (SummonAery)
            {
                if (aery == 0)
                {
                    aery = Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("Aery"), 0, 0,
                               player.whoAmI, -1f) + 1;
                } player.AddBuff(mod.BuffType("Aery"), 2);
            }

            player.statDefense += (int) grasp;

            if (player.HasBuff(mod.BuffType("Aftershock")))
            {
                player.statDefense += player.statDefense;
            }
            
            if (Guardian)
            {
                for (var i = 0; i < Main.player.Length; i++)
                {
                    var player1 = Main.player[i];
                    if (player1.active)
                    {
                        if (player1.whoAmI == player.whoAmI) continue;

                        if (player1.Distance(player.Center) < 300 && player1.team == player.team)
                        {
                            player1.statDefense += getLevel();
                            player1.AddBuff(mod.BuffType("Guardian"), 2);
                            player.statDefense += getLevel();
                            player.AddBuff(mod.BuffType("Guardian"), 2);
                        }
                    }

                }
            }
            
        }

        /*
        public bool hasRune(Rune rune)
        {
            List<Rune>[] primaryRunes = RUNES[primaryPath];
            for (int i = 0; i < 3; i++)
            {
                if (primaryRunes[i][primarySlots[i]] == rune) return true;
            }

            List<Rune>[] secondaryRunes = RUNES[secondaryPath];
            int found = 0;
            int index = 1;
            for (int i = 0; i < 3; i++)
            {
                foreach (Rune rune1 in secondaryRunes[i])
                {
                    if (rune1 == rune) found = index;
                    else index++;
                }
            }
            if (found != 0) return secondarySlots[0] == found || secondarySlots[1] == found;
            
            return false;
        }
        */

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Conquerer)
            {
                if (conquererStacks == 10) Lifesteal(damage * .08f, target, false);
                if (conquererStacks == 9)
                {
                    for (int i = 0; i < 360; i += 3)
                    {
                        Dust.NewDustPerfect(player.Center, 57, player.velocity / 2 + 3 * new Vector2((float) Math.Cos(Math.PI * i / 180.0), .9f * (float) Math.Sin(Math.PI * i / 180.0)));
                    }
                } else if (conquererStacks < 9)
                {
                    for (int i = conquererStacks * 36; i < (conquererStacks + 1) * 36; i += 3)
                    {
                        Dust.NewDustPerfect(player.Center, 57, player.velocity / 2 + 3 * new Vector2((float) Math.Cos(Math.PI * i / 180.0), .9f * (float) Math.Sin(Math.PI * i / 180.0)));
                    }
                }
                conquererStacks++;
                player.AddBuff(mod.BuffType("Conquerer"), 5 * 60);
                if (conquererStacks > 10) conquererStacks = 10;
            }
            if (LethalTempo && !player.HasBuff(mod.BuffType("LethalTempoWindUp")) &&
                !player.HasBuff(mod.BuffType("LethalTempo")) && keystoneCooldown == 0)
            {
                player.AddBuff(mod.BuffType("LethalTempoWindUp"), 120);
                loLPlayer.keystoneCooldown = 60 * 20;
            }

            if ((PressTheAttack || Electrocute || PhaseRush) && keystoneCooldown == 0)
            {
                if (lastHit - 1 != target.whoAmI)
                {
                    tripleStacks = 240;
                    lastHit = target.whoAmI + 1;
                }
                else if (tripleStacks > 240)
                {
                    tripleStacks = 0;
                    if (PressTheAttack)
                    {
                        player.ApplyDamageToNPC(target, Adapative(40 + 10 * getLevel()), 0f, 0, false);
                        keystoneCooldown = 12 * 60;
                        target.AddBuff(mod.BuffType("Exposed"), 6 * 60);
                    } 
                    else if (Electrocute)
                    {
                        int newProjectile = Projectile.NewProjectile(player.Center, new Vector2((target.Center.X - player.Center.X) / 200f, 
                            (target.Center.Y - player.Center.Y) / 200f), mod.ProjectileType("AlarakLightning"), 0, 0f, player.whoAmI, 
                            target.whoAmI);
                        Main.projectile[newProjectile].netUpdate = true;
                        player.ApplyDamageToNPC(target, Adapative(60 + 20 * getLevel()), 0f, 0, false);
                        keystoneCooldown = 60 * (25 - getLevel());
                    }
                    else if (PhaseRush)
                    {
                        player.AddBuff(mod.BuffType("PhaseRush"), 60 * 5);
                        keystoneCooldown = 60 * 15;
                    }
                }
                else tripleStacks = 480;
            }

            if (energized >= 100)
            {
                if (FleetFootwork)
                {
                    player.AddBuff(mod.BuffType("FleetFootwork"), 120);
                    player.statLife += getLevel() * 2;
                    player.HealEffect(getLevel() * 2, false);

                    for (int i = 0; i < 10; i++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, 57);
                    }
                }

                energized = 0;
            }
            energized += 3;

            if (HailOfBlades && target.boss && keystoneCooldown == 0)
            {
                player.AddBuff(mod.BuffType("HailOfBlades"), 30 * 60);
                keystoneCooldown = 4 * 60;
            }

            if (Predator && player.HasBuff(mod.BuffType("Predator")))
            {
                player.DelBuff(player.FindBuffIndex(mod.BuffType("Predator")));
                player.ApplyDamageToNPC(target, Adapative(200 + 25 * getLevel()), 0f, 0, false);
            }

            if (DarkHarvest && player.HasBuff(mod.BuffType("DarkHarvest")) &&
                target.life * 2 < target.lifeMax)
            {
                player.ApplyDamageToNPC(target, Adapative(80 + 20 * getLevel() + darkHarvestSouls * (19 + getLevel())), 0f, 0, false);
                darkHarvestSouls++;
                keystoneCooldown = 60 * 45;

                for (int i = 1; i < 10; i++)
                {
                    Dust.NewDustPerfect(target.Center, 182, target.velocity / 2 + new Vector2(i / 10, i / 10));
                    Dust.NewDustPerfect(target.Center, 182, target.velocity / 2 + new Vector2(i / -10, i / 10));
                    Dust.NewDustPerfect(target.Center, 182, target.velocity / 2 + new Vector2(i / -10, i / -10));
                    Dust.NewDustPerfect(target.Center, 182, target.velocity / 2 + new Vector2(i / 10, i / -10));
                }
            }

            if (ArcaneComet)
            {
                if (keystoneCooldown == 0)
                {
                    Vector2 displacement = target.Center - player.Center;
                    Projectile.NewProjectile(player.Center, displacement / 20, mod.ProjectileType("ArcaneComet"),
                        Adapative(60 + 15 * getLevel()), 0f, player.whoAmI, 19);
                    keystoneCooldown = 30 * (40 - getLevel());
                }
                else
                {
                    keystoneCooldown -= Math.Max(1, (int) 20.0 / proj.maxPenetrate);
                }
            }

            if (SummonAery && keystoneCooldown == 0)
            {
                Main.projectile[aery - 1].ai[0] = target.whoAmI;
                keystoneCooldown = 600;
            }
            
            if (GraspOfTheUndying && player.HasBuff(mod.BuffType("GraspOfTheUndying")) && target.FullName != "Target Dummy")
            {
                player.DelBuff(player.FindBuffIndex(mod.BuffType("GraspOfTheUndying")));
                grasp += .5;
                player.ApplyDamageToNPC(target, Adapative((int) (target.lifeMax * .02)), 0f, 0, false);
                
                player.statLife += (int) (player.statLifeMax2 * .02);
                player.HealEffect((int) (player.statLifeMax2 * .02), false);

                for (int h = 0; h < 5; h++)
                {
                    for (int i = h * 72; i < h * 72 + 10; i++)
                    {
                        Dust d = Dust.NewDustPerfect(
                            target.Center + 40 * new Vector2((float) Math.Cos(Math.PI * i / 180.0),
                                (float) Math.Sin(Math.PI * i / 180.0)),
                            mod.DustType("Green182"),
                            target.velocity / 2 + -4 * new Vector2((float) Math.Cos(Math.PI * i / 180.0),
                                (float) Math.Sin(Math.PI * i / 180.0)));
                        d.noGravity = true;
                    }
                }
            }
            
            if (GlacialAugment && keystoneCooldown == 0)
            {
                keystoneCooldown = 30 * (20 - getLevel());
                List<int> projectiles = new List<int>() {359, 119, 263, 118, 113, ProjectileID.FrostArrow, 253, 166};
                int chosen = projectiles[Main.rand.Next(projectiles.Count - 1)];

                Vector2 d = target.Center - player.Center;
                d.Normalize();

                Projectile.NewProjectile(player.Center, d * 10f * (chosen == 263 || chosen == 166 ? 2f : 1f), chosen, Adapative(40 + 15 * getLevel()), 5f,
                    player.whoAmI);
            }

            if (Kleptomancy && keystoneCooldown == 0 && target.value > 0)
            {
                keystoneCooldown = 60 * 10;
                player.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(getLevel(), getLevel() * getLevel()));
                if (Main.rand.Next(5) == 0)
                {
                    int rand = Main.rand.Next(1, 4);
                    switch (rand)
                    {
                        case 1:
                            if (!Main.hardMode)
                            {
                                player.QuickSpawnItem(ItemID.LesserHealingPotion);
                            } else if (NPC.downedMoonlord)
                            {
                                player.QuickSpawnItem(ItemID.SuperHealingPotion);
                            } else if (NPC.downedMechBossAny)
                            {
                                player.QuickSpawnItem(ItemID.GreaterHealingPotion);
                            } else player.QuickSpawnItem(ItemID.HealingPotion);
                            break;
                        case 2:
                            if (!Main.hardMode)
                            {
                                player.QuickSpawnItem(ItemID.LesserManaPotion);
                            } else if (NPC.downedMoonlord)
                            {
                                player.QuickSpawnItem(ItemID.SuperManaPotion);
                            } else if (NPC.downedMechBossAny)
                            {
                                player.QuickSpawnItem(ItemID.GreaterManaPotion);
                            } else player.QuickSpawnItem(ItemID.ManaPotion);
                            break;
                        case 3:
                            if (!Main.hardMode)
                            {
                                player.QuickSpawnItem(ItemID.LesserRestorationPotion);
                            } else if (NPC.downedMechBossAny)
                            {
                            } else player.QuickSpawnItem(ItemID.RestorationPotion);
                            break;
                        case 4:
                            if (!Main.hardMode)
                            {
                                List<int> potions = new List<int>()
                                {
                                    ItemID.ArcheryPotion, ItemID.BattlePotion, ItemID.FeatherfallPotion, ItemID.FlipperPotion, ItemID.GillsPotion, ItemID.GravitationPotion, ItemID.TrapsightPotion,
                                    ItemID.HunterPotion, ItemID.IronskinPotion, ItemID.MagicPowerPotion, ItemID.ManaRegenerationPotion, ItemID.MiningPotion, ItemID.NightOwlPotion, ItemID.FishingPotion,
                                    ItemID.ObsidianSkinPotion, ItemID.RegenerationPotion, ItemID.ShinePotion, ItemID.SpelunkerPotion, ItemID.SwiftnessPotion, ItemID.ThornsPotion, ItemID.WaterWalkingPotion,
                                    ItemID.CratePotion, ItemID.SonarPotion, ItemID.SummoningPotion, ItemID.RecallPotion, ItemID.TeleportationPotion, ItemID.WormholePotion
                                };
                                player.QuickSpawnItem(potions[Main.rand.Next(potions.Count - 1)]);
                            }
                            else
                            {
                                List<int> potions = new List<int>()
                                {
                                    ItemID.AmmoReservationPotion, ItemID.EndurancePotion, ItemID.HeartreachPotion, ItemID.InfernoPotion, ItemID.LifeforcePotion, ItemID.RagePotion, ItemID.SummoningPotion,
                                    ItemID.WarmthPotion, ItemID.WrathPotion, ItemID.IronskinPotion, ItemID.SwiftnessPotion, ItemID.MagicPowerPotion, ItemID.ManaRegenerationPotion, ItemID.SummoningPotion,
                                    ItemID.RegenerationPotion, ItemID.WormholePotion
                                };
                                player.QuickSpawnItem(potions[Main.rand.Next(potions.Count - 1)]);
                            }
                            break;
                    }
                }
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (loLPlayer.Conquerer)
            {
                if (conquererStacks == 10) Lifesteal(damage * .15f, target, false);
                if (conquererStacks == 8 || conquererStacks == 9)
                {
                    for (int i = 0; i < 360; i += 3)
                    {
                        Dust.NewDustPerfect(player.Center, 57, player.velocity / 2 + 3 * new Vector2((float) Math.Cos(Math.PI * i / 180.0), .9f * (float) Math.Sin(Math.PI * i / 180.0)));
                    }
                } else if (conquererStacks < 8)
                {
                    for (int i = conquererStacks * 36; i < (conquererStacks + 2) * 36; i += 3)
                    {
                        Dust.NewDustPerfect(player.Center, 57, player.velocity / 2 + 3 * new Vector2((float) Math.Cos(Math.PI * i / 180.0), .9f * (float) Math.Sin(Math.PI * i / 180.0)));
                    }
                }
                conquererStacks += 2;
                player.AddBuff(mod.BuffType("Conquerer"), 5 * 60);
                if (conquererStacks > 10) conquererStacks = 10;
            }
            if (LethalTempo && !player.HasBuff(mod.BuffType("LethalTempoWindUp")) &&
                !player.HasBuff(mod.BuffType("LethalTempo")) && keystoneCooldown == 0)
            {
                player.AddBuff(mod.BuffType("LethalTempoWindUp"), 120);
                loLPlayer.keystoneCooldown = 60 * 20;
            }
            
            if ((PressTheAttack || Electrocute || PhaseRush) && keystoneCooldown == 0)
            {
                if (lastHit - 1 != target.whoAmI)
                {
                    tripleStacks = 240;
                    lastHit = target.whoAmI + 1;
                }
                else if (tripleStacks > 240)
                {
                    tripleStacks = 0;
                    if (PressTheAttack)
                    {
                        player.ApplyDamageToNPC(target, Adapative(40 + 10 * getLevel()), 0f, 0, false);
                        keystoneCooldown = 12 * 60;
                        target.AddBuff(mod.BuffType("Exposed"), 6 * 60);
                    } 
                    else if (Electrocute)
                    {
                        int newProjectile = Projectile.NewProjectile(player.Center, new Vector2((target.Center.X - player.Center.X) / 200f, 
                                (target.Center.Y - player.Center.Y) / 200f), mod.ProjectileType("AlarakLightning"), 0, 0f, player.whoAmI, 
                            target.whoAmI);
                        Main.projectile[newProjectile].netUpdate = true;
                        player.ApplyDamageToNPC(target, Adapative(80 + 20 * getLevel()), 0f, 0, false);
                        keystoneCooldown = 60 * (25 - getLevel());
                    } 
                    else if (PhaseRush)
                    {
                        player.AddBuff(mod.BuffType("PhaseRush"), 5 * 60);
                        keystoneCooldown = 15 * 60;
                    }
                }
                else tripleStacks = 480;
            }

            if (energized >= 100)
            {
                if (FleetFootwork)
                {
                    player.AddBuff(mod.BuffType("FleetFootwork"), 120);
                    player.statLife += getLevel() * 3;
                    player.HealEffect(getLevel() * 3, false);
                    
                    for (int i = 0; i < 10; i++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, 57);
                    }
                }

                energized = 0;
            }
            else energized += 10;
            
            if (HailOfBlades && target.boss && keystoneCooldown == 0)
            {
                player.AddBuff(mod.BuffType("HailOfBlades"), 30 * 60);
                keystoneCooldown = 4 * 60;
            }

            if (Predator && player.HasBuff(mod.BuffType("Predator")))
            {
                player.DelBuff(player.FindBuffIndex(mod.BuffType("Predator")));
                player.ApplyDamageToNPC(target, Adapative(200 + 25 * getLevel()), 0f, 0, false);
            }
            
            if (DarkHarvest && player.HasBuff(mod.BuffType("DarkHarvest")) &&
                target.life * 2 < target.lifeMax)
            {
                player.ApplyDamageToNPC(target, Adapative(80 + 20 * getLevel() + darkHarvestSouls * (19 + getLevel())), 0f, 0, false);
                darkHarvestSouls++;
                keystoneCooldown = 60 * 45;
                
                for (int i = 1; i < 10; i++)
                {
                    Dust.NewDustPerfect(target.Center, 182, target.velocity / 2 + new Vector2(i / 10, i / 10));
                    Dust.NewDustPerfect(target.Center, 182, target.velocity / 2 + new Vector2(i / -10, i / 10));
                    Dust.NewDustPerfect(target.Center, 182, target.velocity / 2 + new Vector2(i / -10, i / -10));
                    Dust.NewDustPerfect(target.Center, 182, target.velocity / 2 + new Vector2(i / 10, i / -10));
                }
            }
            
            if (SummonAery && keystoneCooldown == 0)
            {
                Main.projectile[aery - 1].ai[0] = target.whoAmI;
                keystoneCooldown = 600;
            }

            if (GraspOfTheUndying && player.HasBuff(mod.BuffType("GraspOfTheUndying")) && target.FullName != "Target Dummy")
            {
                player.DelBuff(player.FindBuffIndex(mod.BuffType("GraspOfTheUndying")));
                grasp++;
                player.ApplyDamageToNPC(target, Adapative((int) (target.lifeMax * .04)), 0f, 0, false);
                
                player.statLife += (int) (player.statLifeMax2 * .04);
                player.HealEffect((int) (player.statLifeMax2 * .04), false);
                
                for (int h = 0; h < 5; h++)
                {
                    for (int i = h * 72; i < h * 72 + 20; i++)
                    {
                        Dust d = Dust.NewDustPerfect(
                            target.Center + 40 * new Vector2((float) Math.Cos(Math.PI * i / 180.0),
                                (float) Math.Sin(Math.PI * i / 180.0)),
                            mod.DustType("Green182"),
                            target.velocity / 2 + -4 * new Vector2((float) Math.Cos(Math.PI * i / 180.0),
                                (float) Math.Sin(Math.PI * i / 180.0)));
                        d.noGravity = true;
                    }
                }
            }

            if (GlacialAugment && keystoneCooldown == 0)
            {
                keystoneCooldown = 30 * (20 - getLevel());
                List<int> projectiles = new List<int>() {359, 119, 263, 118, 113, ProjectileID.FrostArrow, 253, 166};
                int chosen = projectiles[Main.rand.Next(projectiles.Count - 1)];

                Vector2 d = target.Center - player.Center;
                d.Normalize();

                Projectile.NewProjectile(player.Center, d * 10f, chosen, Adapative(40 + 15 * getLevel()), 5f,
                    player.whoAmI);
            }
            
            if (Kleptomancy && keystoneCooldown == 0 && target.value > 0)
            {
                keystoneCooldown = 60 * 10;
                player.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(getLevel(), getLevel() * getLevel()));
                if (Main.rand.Next(5) == 0)
                {
                    int rand = Main.rand.Next(1, 4);
                    switch (rand)
                    {
                        case 1:
                            if (!Main.hardMode)
                            {
                                player.QuickSpawnItem(ItemID.LesserHealingPotion);
                            } else if (NPC.downedMoonlord)
                            {
                                player.QuickSpawnItem(ItemID.SuperHealingPotion);
                            } else if (NPC.downedMechBossAny)
                            {
                                player.QuickSpawnItem(ItemID.GreaterHealingPotion);
                            } else player.QuickSpawnItem(ItemID.HealingPotion);
                            break;
                        case 2:
                            if (!Main.hardMode)
                            {
                                player.QuickSpawnItem(ItemID.LesserManaPotion);
                            } else if (NPC.downedMoonlord)
                            {
                                player.QuickSpawnItem(ItemID.SuperManaPotion);
                            } else if (NPC.downedMechBossAny)
                            {
                                player.QuickSpawnItem(ItemID.GreaterManaPotion);
                            } else player.QuickSpawnItem(ItemID.ManaPotion);
                            break;
                        case 3:
                            if (!Main.hardMode)
                            {
                                player.QuickSpawnItem(ItemID.LesserRestorationPotion);
                            } else if (NPC.downedMechBossAny)
                            {
                            } else player.QuickSpawnItem(ItemID.RestorationPotion);
                            break;
                        case 4:
                            if (!Main.hardMode)
                            {
                                List<int> potions = new List<int>()
                                {
                                    ItemID.ArcheryPotion, ItemID.BattlePotion, ItemID.FeatherfallPotion, ItemID.FlipperPotion, ItemID.GillsPotion, ItemID.GravitationPotion, ItemID.TrapsightPotion,
                                    ItemID.HunterPotion, ItemID.IronskinPotion, ItemID.MagicPowerPotion, ItemID.ManaRegenerationPotion, ItemID.MiningPotion, ItemID.NightOwlPotion, ItemID.FishingPotion,
                                    ItemID.ObsidianSkinPotion, ItemID.RegenerationPotion, ItemID.ShinePotion, ItemID.SpelunkerPotion, ItemID.SwiftnessPotion, ItemID.ThornsPotion, ItemID.WaterWalkingPotion,
                                    ItemID.CratePotion, ItemID.SonarPotion, ItemID.SummoningPotion, ItemID.RecallPotion, ItemID.TeleportationPotion, ItemID.WormholePotion
                                };
                                player.QuickSpawnItem(potions[Main.rand.Next(potions.Count - 1)]);
                            }
                            else
                            {
                                List<int> potions = new List<int>()
                                {
                                    ItemID.AmmoReservationPotion, ItemID.EndurancePotion, ItemID.HeartreachPotion, ItemID.InfernoPotion, ItemID.LifeforcePotion, ItemID.RagePotion, ItemID.SummoningPotion,
                                    ItemID.WarmthPotion, ItemID.WrathPotion, ItemID.IronskinPotion, ItemID.SwiftnessPotion, ItemID.MagicPowerPotion, ItemID.ManaRegenerationPotion, ItemID.SummoningPotion,
                                    ItemID.RegenerationPotion, ItemID.WormholePotion
                                };
                                player.QuickSpawnItem(potions[Main.rand.Next(potions.Count - 1)]);
                            }
                            break;
                    }
                }
            }
        }
        
        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (damage > 0 && GraspOfTheUndying && keystoneCooldown == 0)
            {
                player.AddBuff(mod.BuffType("GraspOfTheUndying"), 4 * 60);
                loLPlayer.keystoneCooldown = 60 * 20;
            }
            
            if (damage > 0 && Aftershock && keystoneCooldown == 0)
            {
                player.AddBuff(mod.BuffType("Aftershock"), 5 * 60);
                loLPlayer.keystoneCooldown = 60 * 20;
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Predator && DoomBubblesMod.PredatorHotKey.JustPressed && keystoneCooldown == 0
                && hasBoots())
            {
                player.AddBuff(mod.BuffType("Predator"), 30 * 60);
                keystoneCooldown = 60 * (30 + (20 - getLevel()) * 5);
            }
        }

        public override void PostItemCheck()
        {
            Item item = player.inventory[player.selectedItem];
            if ((item.type == ItemID.LunarFlareBook || item.type == ItemID.CrystalStorm) && player.GetModPlayer<LoLPlayer>().UnsealedSpellbook && player.itemAnimation == player.itemAnimationMax - 1)
            {
                Vector2 position2 = player.RotatedRelativePoint(player.MountedCenter);
                float num70 = (float)Main.mouseX + Main.screenPosition.X - position2.X;
                float num71 = (float)Main.mouseY + Main.screenPosition.Y - position2.Y;
                if (player.gravDir == -1f)
                {
                    num71 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - position2.Y;
                }
                float num72 = (float)Math.Sqrt(num70 * num70 + num71 * num71);
                if ((float.IsNaN(num70) && float.IsNaN(num71)) || (num70 == 0f && num71 == 0f))
                {
                    num70 = player.direction;
                    num71 = 0f;
                    num72 = item.shootSpeed;
                }
                else
                {
                    num72 = item.shootSpeed / num72;
                }
                num70 *= num72;
                num71 *= num72;
                Shoot(item, ref position2, ref num70, ref num71, ref item.shoot, ref item.damage, ref item.knockBack);
            }
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            List<int> spellBooks = new List<int>() {ItemID.WaterBolt, ItemID.BookofSkulls, ItemID.DemonScythe, ItemID.CursedFlames, 
                ItemID.GoldenShower, ItemID.CrystalStorm, ItemID.MagnetSphere, ItemID.RazorbladeTyphoon, ItemID.LunarFlareBook, mod.ItemType("FlamestrikeTome")};
            if (UnsealedSpellbook && spellBooks.Contains(item.type) && keystoneCooldown == 0)
            {
                keystoneCooldown = 30 * (20 - getLevel());
                foreach (var item1 in player.inventory)
                {
                    if (item1.type == item.type || !spellBooks.Contains(item1.type)) continue;

                    if (!spellbook.ContainsKey(item1.type) || spellbook[item1.type] == 0)
                    {
                        spellbook[item1.type] = item1.useTime;
                        Main.PlaySound(item1.UseSound, position);
                        for (int i = 0; i < item.useTime; i += item1.useTime)
                        {
                            Vector2 speed = new Vector2(speedX, speedY);
                            speed.Normalize();
                            if (item1.type == ItemID.LunarFlareBook)
                            {
                                int num105 = 3;
                                for (int num106 = 0; num106 < num105; num106++)
                                {
                                    Vector2 position2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                                    position2.X = (position.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
                                    position2.Y -= 100 * num106;
                                    float num70 = (float)Main.mouseX + Main.screenPosition.X - position2.X;
                                    float num71 = (float)Main.mouseY + Main.screenPosition.Y - position2.Y;
                                    float ai2 = num71 + position2.Y;
                                    if (num71 < 0f)
                                    {
                                        num71 *= -1f;
                                    }
                                    if (num71 < 20f)
                                    {
                                        num71 = 20f;
                                    }
                                    float num72 = (float)Math.Sqrt(num70 * num70 + num71 * num71);
                                    num72 = item1.shootSpeed / num72;
                                    num70 *= num72;
                                    num71 *= num72;
                                    Vector2 vector9 = new Vector2(num70, num71) / 2f;
                                    Projectile.NewProjectile(position2.X, position2.Y, vector9.X, vector9.Y, item1.shoot, player.GetWeaponDamage(item1), player.GetWeaponKnockback(item1, item1.knockBack), player.whoAmI, 0f, ai2);
                                }
                                
                            }
                            else if (item1.type == ItemID.CrystalStorm)
                            {
                                Vector2 position2 = player.RotatedRelativePoint(player.MountedCenter);
                                float num70 = (float)Main.mouseX + Main.screenPosition.X - position2.X;
                                float num71 = (float)Main.mouseY + Main.screenPosition.Y - position2.Y;
                                if (player.gravDir == -1f)
                                {
                                    num71 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - position2.Y;
                                }
                                float num72 = (float)Math.Sqrt(num70 * num70 + num71 * num71);
                                if ((float.IsNaN(num70) && float.IsNaN(num71)) || (num70 == 0f && num71 == 0f))
                                {
                                    num70 = player.direction;
                                    num71 = 0f;
                                    num72 = item1.shootSpeed;
                                }
                                else
                                {
                                    num72 = item1.shootSpeed / num72;
                                }
                                num70 *= num72;
                                num71 *= num72;
                                
                                float num114 = num70;
                                float num115 = num71;
                                num114 += (float)Main.rand.Next(-40, 41) * 0.04f;
                                num115 += (float)Main.rand.Next(-40, 41) * 0.04f;
                                Projectile.NewProjectile(position2.X, position2.Y, num114, num115, item1.shoot, player.GetWeaponDamage(item1), player.GetWeaponKnockback(item1, item1.knockBack), player.whoAmI);
                            }
                            else if (item.type == mod.ItemType("FlamestrikeTome"))
                            {
                                FlamestrikeTome tome = (FlamestrikeTome) item1.modItem;
                                Vector2 position2 = Main.MouseWorld;
                                int proj = Projectile.NewProjectile(position2, new Vector2(0, 0), item1.shoot, 
                                    player.GetWeaponDamage(item1), player.GetWeaponKnockback(item1, item1.knockBack), player.whoAmI, tome.ChosenTalent, tome.Verdant);
                                Main.projectile[proj].netUpdate = true;
                            }
                            else
                            {
                                Projectile.NewProjectile(position, speed * item1.shootSpeed, item1.shoot, player.GetWeaponDamage(item1),
                                    player.GetWeaponKnockback(item1, item1.knockBack), player.whoAmI);
                            }
                        }
                    }
                }
            }
            
            return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        
        
        public void Lifesteal(float healAmount, NPC npc, bool aoe)
        {
            if (!npc.immortal && npc.lifeMax > 5 && !player.moonLeech && npc.FullName != "Target Dummy" &&
                player.lifeSteal > 0f && player.statLife < player.statLifeMax2)
            {
                if (aoe) healAmount /= 3f;
                player.lifeSteal -= healAmount;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ProjectileID.VampireHeal,
                    0, 0f, player.whoAmI, player.whoAmI, healAmount);
            }
        }

        public bool hasBoots()
        {
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].shoeSlot > 0) return true;
            }

            return false;
        }

        public int getLevel()
        {
            int level = 1;
            level += NPC.downedBoss1 ? 1 : 0;
            level += NPC.downedBoss2 ? 1 : 0;
            level += NPC.downedBoss3 ? 1 : 0;
            level += NPC.downedMoonlord ? 1 : 0;
            level += NPC.downedFishron ? 1 : 0;
            level += NPC.downedGolemBoss ? 1 : 0;
            level += NPC.downedSlimeKing ? 1 : 0;
            level += NPC.downedMechBoss1 ? 1 : 0;
            level += NPC.downedMechBoss2 ? 1 : 0;
            level += NPC.downedMechBoss3 ? 1 : 0;
            level += NPC.downedPlantBoss ? 1 : 0;
            level += NPC.downedQueenBee ? 1 : 0;
            level += NPC.downedAncientCultist ? 1 : 0;
            level += NPC.downedHalloweenKing ? 1 : 0;
            level += NPC.downedChristmasIceQueen ? 1 : 0;
            level += Main.hardMode ? 1 : 0;
            level += NPC.downedMartians ? 1 : 0;
            level += NPC.downedGoblins ? 1 : 0;
            level += NPC.downedPirates ? 1 : 0;
            return level;
        }

        public int Adapative(int input)
        {
            float calc = input;
            calc *= player.allDamage;
            calc *= Math.Max(player.magicDamage,
                Math.Max(player.meleeDamage, Math.Max(player.rangedDamage, player.minionDamage)));
            calc *= player.allDamageMult;
            calc *= Math.Max(player.magicDamageMult,
                Math.Max(player.meleeDamageMult, Math.Max(player.rangedDamageMult, player.minionDamageMult)));
            return (int) calc;
        }
    }

    public enum RunePath
    {
        None = 0,
        Precision = 1,
        Domination = 2,
        Sorcery = 3,
        Resolve = 4,
        Inspiration = 5
    }

    /*
    public enum Rune
    {
        Overheal,
        Triumph,
        PresenceOfMind,
        LegendAlacrity,
        LegendTenacity,
        LegendBloodline,
        CoupDeGrace,
        CutDown,
        LastStand,
        
        CheapShot,
        TasteOfBlood,
        SuddenImpact,
        ZombieWard,
        GhostPoro,
        EyeballCollection,
        RavenousHunter,
        IngeniousHunter,
        RelentlessHunter,
        UltimateHunter,
        
        NullifyingOrb,
        ManaflowBand,
        NimbusCloak,
        Transendence,
        Celerity,
        AbsoluteFocus,
        Scorch,
        Waterwalking,
        GatheringStorm,
        
        Demolish,
        FontOfLife,
        ShieldBash,
        Conditioning,
        SecondWind,
        BonePlating,
        Overgrowth,
        Revitalize,
        Unflinching,
        
        HextechFlashtraption,
        MagicalFootwear,
        PerfectingTiming,
        FuturesMarket,
        MinionDematerializer,
        BiscuitDelivery,
        CosmicInsight,
        ApproachVelocity,
        TimeWarpTonic

    }
    */
}