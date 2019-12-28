using System;
using System.Collections.Generic;
using DoomBubblesMod.Items.Thanos;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod
{
    public class ThanosPlayer : ModPlayer
    {
        public int gem = -1;
        public int tbMouseX;
        public int tbMouseY;
        public bool soulStone;
        public Tile soulStoneTile = new Tile();
        public bool soulStoneTileActive;
        public int[] timeHealth = new int[302];
        public int powerStoned;
        public int powerStoneCharge;
        public bool powerStone;
        public List<int> powerStoning = new List<int>();

        public bool INfinityGauntlet;

        public override void ResetEffects()
        {
            powerStone = false;
            if (powerStoned > 0)
            {
                powerStoned--;
            }

            powerStoning.RemoveAll(i => !Main.npc[i].HasBuff(mod.BuffType("PowerStoneDebuff")));
            INfinityGauntlet = false;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            base.ProcessTriggers(triggersSet);
            Vector2 gauntlet = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);

            if (INfinityGauntlet && player.itemTime == 0 && player.itemAnimation == 0)
            {
                if (DoomBubblesMod.PowerStoneHotKey.Current)
                {
                    if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                    {
                        player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                        if (Main.time % 10 == 0)
                        {
                            Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 34,
                                .3f + (float) (.05 * Math.Sqrt(player.GetModPlayer<ThanosPlayer>().powerStoneCharge)));
                        }
                    }

                    if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge > 100 &&
                        player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                    {
                        player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                    }

                    if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge > 200 &&
                        player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                    {
                        player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                    }

                    if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge == 300)
                    {
                        if (Main.time % 10 == 0)
                        {
                            Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 15, 1f);
                            for (int i = 0; i <= 360; i += 4)
                            {
                                double rad = (Math.PI * i) / 180;
                                float dX = (float) (10 * Math.Cos(rad));
                                float dY = (float) (10 * Math.Sin(rad));
                                Dust dust = Dust.NewDustPerfect(new Vector2(gauntlet.X + 10 * dX, gauntlet.Y + 10 * dY),
                                    212,
                                    new Vector2(dX * -1f + player.velocity.X, dY * -1f + player.velocity.Y), 0,
                                    InfinityGauntlet.power);
                                dust.noGravity = true;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < player.GetModPlayer<ThanosPlayer>().powerStoneCharge / 10; i++)
                        {
                            double rad = Math.PI * Main.rand.NextDouble() * 2;
                            float dX = (float) (10 * Math.Cos(rad));
                            float dY = (float) (10 * Math.Sin(rad));

                            Dust dust = Dust.NewDustPerfect(new Vector2(gauntlet.X + 10 * dX, gauntlet.Y + 10 * dY),
                                212,
                                new Vector2(dX * -1f + player.velocity.X, dY * -1f + player.velocity.Y), 0,
                                InfinityGauntlet.power);
                            dust.noGravity = true;
                        }
                    }
                }
                else if (DoomBubblesMod.PowerStoneHotKey.JustReleased)
                {
                    Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 74, 2f);
                    Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 89, 2f);
                    Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 93, .5f);
                    player.AddBuff(mod.BuffType("PowerStone"),
                        6 * player.GetModPlayer<ThanosPlayer>().powerStoneCharge);
                    for (int i = 0; i <= 360; i += 5)
                    {
                        double rad = (Math.PI * i) / 180;
                        float dX = (float) (20 * Math.Cos(rad));
                        float dY = (float) (20 * Math.Sin(rad));
                        Projectile.NewProjectile(gauntlet, new Vector2(dX, dY), mod.ProjectileType("PowerExplosion"),
                            (int) (player.GetModPlayer<ThanosPlayer>().powerStoneCharge * 3.33333333f), 5,
                            player.whoAmI);
                    }


                    player.GetModPlayer<ThanosPlayer>().powerStoneCharge = 0;
                } else if (DoomBubblesMod.SpaceStoneHotKey.JustPressed &&
                           !player.HasBuff(mod.BuffType("SpaceStoneCooldown")) &&
                           !Collision.SolidCollision(Main.MouseWorld, player.width, player.height)
                           && Main.MouseWorld.X > 50f && Main.MouseWorld.X < Main.maxTilesX * 16 - 50 &&
                               Main.MouseWorld.Y > 50f && Main.MouseWorld.Y < Main.maxTilesY * 16 - 50)
                {
                    Vector2 newPos = Main.MouseWorld;
            
                    Projectile.NewProjectileDirect(player.Center, new Vector2(0, 0), mod.ProjectileType("SpaceStoneWormhole"), 0, 0, player.whoAmI);
                    for (int i = 0; i <= 360; i += 4)
                    {
                        double rad = (Math.PI * i) / 180;
                        float dX = (float) (12 * Math.Cos(rad));
                        float dY = (float) (12 * Math.Sin(rad));
                        Dust dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY), 0, InfinityGauntlet.space, 1.5f);
                        dust.noGravity = true;
                    }
                    
                    player.Teleport(newPos, -1);
                    
                    Projectile.NewProjectileDirect(player.Center, new Vector2(0, 0), mod.ProjectileType("SpaceStoneWormhole"), 0, 0, player.whoAmI);
                    for (int i = 0; i <= 360; i += 4)
                    {
                        double rad = (Math.PI * i) / 180;
                        float dX = (float) (12 * Math.Cos(rad));
                        float dY = (float) (12 * Math.Sin(rad));
                        Dust dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY), 0, InfinityGauntlet.space, 1.5f);
                        dust.noGravity = true;
                    }
                    player.AddBuff(mod.BuffType("SpaceStoneCooldown"), 360);
                    Main.PlaySound(SoundID.Item8, player.Center);
                } else if (DoomBubblesMod.RealityStoneHotKey.Current)
                {
                    if (Main.time % 15 == 0)
                    {
                        Main.PlaySound(2, (int) player.Center.X, (int) player.Center.Y, 103, 1f);
                    }

                    Vector2 mousePos = Main.MouseWorld;
                    double theta = Main.rand.NextDouble() * 2 * Math.PI;
                    double x = mousePos.X + Main.rand.NextDouble() * 40 * Math.Cos(theta);
                    double y = mousePos.Y + Main.rand.NextDouble() * 40 * Math.Sin(theta);
                    Projectile.NewProjectile((float) x, (float) y, 0f, 0f, mod.ProjectileType("RealityBeam"), 100, 0, player.whoAmI);
                } else if (DoomBubblesMod.SoulStoneHotKey.JustPressed)
                {
                    if (player.chest == -3)
                    {
                        player.chest = -1;
                        Recipe.FindRecipes();
                    }
                    else
                    {
                        player.chest = -3;
                        player.chestX = (int) (player.position.X / 16f);
                        player.chestY = (int) (player.position.Y / 16f);
                        player.talkNPC = -1;
                        Main.npcShop = 0;
                        Main.playerInventory = true;
                        Main.recBigList = false;
                        Recipe.FindRecipes();
                        player.GetModPlayer<ThanosPlayer>().soulStone = true;
                    }
                } else if (DoomBubblesMod.TimeStoneHotKey.JustPressed)
                {
                    int previousHp = player.GetModPlayer<ThanosPlayer>().timeHealth[300];
                    if (previousHp > player.statLife && !player.HasBuff(mod.BuffType("TimeStoneCooldown")))
                    {
                        player.HealEffect(previousHp - player.statLife);
                        player.statLife = previousHp;
                        for (int i = 0; i <= 360; i += 4)
                        {
                            double rad = (Math.PI * i) / 180;
                            float dX = (float) (5 * Math.Cos(rad));
                            float dY = (float) (5 * Math.Sin(rad));
                            Dust dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY), 0, InfinityGauntlet.time);
                            dust.noGravity = true;
                        }
                        Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 15, 2f);
                        player.AddBuff(mod.BuffType("TimeStoneCooldown"), 1800);
                    }
                } else if (DoomBubblesMod.MindStoneHotKey.JustPressed && !player.HasBuff(mod.BuffType("MindStoneCooldown")))
                {
                    Vector2 newPos = Main.MouseWorld;
            
                    for (int i = 0; i <= 360; i += 3)
                    {
                        double rad = (Math.PI * i) / 180;
                        float dX = (float) (9 * Math.Cos(rad));
                        float dY = (float) (9 * Math.Sin(rad));
                        Dust dust = Dust.NewDustPerfect(newPos, 212, new Vector2(dX, dY), 0, InfinityGauntlet.mind, 1.5f);
                        dust.noGravity = true;
                    }
                    for (int i = 0; i <= 360; i += 3)
                    {
                        double rad = (Math.PI * i) / 180;
                        float dX = (float) (9 * Math.Cos(rad));
                        float dY = (float) (9 * Math.Sin(rad));
                        Dust dust = Dust.NewDustPerfect(new Vector2(newPos.X + (11 * dX), newPos.Y + (11 * dY)), 212, new Vector2(-dX, -dY), 0, InfinityGauntlet.mind, 1.5f);
                        dust.noGravity = true;
                    }
            
                    Main.PlaySound(SoundLoader.customSoundType, (int) player.position.X, (int) player.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/MindStone"));
            
                    foreach (NPC npc in Main.npc)
                    {
                        if (npc.Distance(newPos) < 100f && !npc.friendly && npc.TypeName != "Target Dummy" && !npc.boss)
                        {
                            npc.damage = 0;
                            npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly = true;
                            if (Main.netMode == 1)
                            {
                                ModPacket packet = mod.GetPacket();
                                packet.Write((byte)DoomBubblesModMessageType.infinityStone);
                                packet.Write(npc.whoAmI);
                                packet.Write(2);
                                packet.Send();
                            }
                        }
                    }
            
            
                    player.AddBuff(mod.BuffType("MindStoneCooldown"), 1200);
                }
            }
        }

        public override void PreUpdate()
        {
            if (Main.time % 60 == 0)
            {
                foreach (int i in powerStoning)
                {
                    powerStoneDamage(i, .1f);
                }
            }

            if (soulStone)
            {
                player.chestX = (int) (player.position.X / 16f);
                player.chestY = (int) (player.position.Y / 16f);
                soulStoneTile = Main.tile[(int) (player.position.X / 16f), (int) (player.position.Y / 16f)];
                soulStoneTileActive = soulStoneTile.active();
                if (!soulStoneTileActive)
                {
                    soulStoneTile.active(true);
                }
            }


            for (int i = 300; i > 0; i--)
            {
                timeHealth[i] = timeHealth[i - 1];
            }

            timeHealth[0] = player.statLife;

            base.PreUpdate();
        }

        public override void PostUpdate()
        {
            if (soulStone)
            {
                if (!soulStoneTileActive)
                {
                    soulStoneTile.active(false);
                }
            }


            if (player.chest != -3)
            {
                soulStone = false;
            }


            base.PostUpdate();
        }

        public override void UpdateDead()
        {
            powerStoning = new List<int>();
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (player.GetModPlayer<ThanosPlayer>().powerStone)
            {
                if (target.HasBuff(mod.BuffType("PowerStoneDebuff")))
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoneDamage(target.whoAmI, .01f);
                }

                target.AddBuff(mod.BuffType("PowerStoneDebuff"), 300);
                if (!player.GetModPlayer<ThanosPlayer>().powerStoning.Contains(target.whoAmI))
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoning.Add(target.whoAmI);
                }
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback,
            ref bool crit,
            ref int hitDirection)
        {
            if (player.GetModPlayer<ThanosPlayer>().powerStone && proj.type != mod.ProjectileType("PowerStone"))
            {
                player.GetModPlayer<ThanosPlayer>().powerStoneDamage(target.whoAmI, .01f);
                target.AddBuff(mod.BuffType("PowerStoneDebuff"), 300);
                if (!player.GetModPlayer<ThanosPlayer>().powerStoning.Contains(target.whoAmI))
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoning.Add(target.whoAmI);
                }
            }
        }

        public void powerStoneDamage(int npcId, float multiplier)
        {
            NPC npc = Main.npc[npcId];
            int damage = 1000 + Math.Min((int) (npc.lifeMax * .001), 9000);
            damage = (int) (damage * multiplier);
            float knockback = 0f;
            int direction = 0;
            bool crit = false;

            int combatText = -1;
            if (Main.netMode != 2)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (Main.combatText[i].active)
                    {
                        continue;
                    }

                    combatText = i;
                    break;
                }
            }

            player.ApplyDamageToNPC(npc, damage, knockback, direction, crit);
            player.addDPS(damage);
            if (combatText != -1 && Main.combatText[combatText].active)
            {
                Main.combatText[combatText].color = InfinityGauntlet.power;
                Main.combatText[combatText].crit = multiplier > .5f;
                Main.combatText[combatText].dot = multiplier > .5f;
            }
        }
    }
}