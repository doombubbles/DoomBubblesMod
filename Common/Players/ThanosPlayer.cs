using System;
using System.Collections.Generic;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Content.Items.Thanos;
using DoomBubblesMod.Content.Projectiles.Thanos;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using PowerStone = DoomBubblesMod.Content.Buffs.PowerStone;
using Terraria.ID;

namespace DoomBubblesMod.Common.Players;

public class ThanosPlayer : ModPlayer
{
    public int gem = -1;

    public Item infinityGauntlet;
    public bool powerStone;
    public int powerStoneCharge;
    public int powerStoned;
    public HashSet<int> powerStoning = new();
    public bool soulStone;
    public Tile soulStoneTile = new();
    public bool soulStoneTileActive;
    public int[] timeHealth = new int[302];

    public override void ResetEffects()
    {
        powerStone = false;
        if (powerStoned > 0)
        {
            powerStoned--;
        }

        powerStoning.RemoveWhere(i => !Main.npc[i].HasBuff(ModContent.BuffType<PowerStoneDebuff>()));
        infinityGauntlet = null;
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        base.ProcessTriggers(triggersSet);
        var gauntlet = new Vector2(Player.Center.X + 10 * Player.direction, Player.Center.Y - 25);

        if (infinityGauntlet != null && Player.itemTime == 0 && Player.itemAnimation == 0)
        {
            if (DoomBubblesMod.powerStoneHotKey.Current)
            {
                if (Player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                {
                    Player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                    if (Main.time % 10 == 0)
                    {
                        SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 34,
                            .3f + (float) (.05 * Math.Sqrt(Player.GetModPlayer<ThanosPlayer>().powerStoneCharge)));
                    }
                }

                if (Player.GetModPlayer<ThanosPlayer>().powerStoneCharge > 100 &&
                    Player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                {
                    Player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                }

                if (Player.GetModPlayer<ThanosPlayer>().powerStoneCharge > 200 &&
                    Player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                {
                    Player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                }

                if (Player.GetModPlayer<ThanosPlayer>().powerStoneCharge == 300)
                {
                    if (Main.time % 10 == 0)
                    {
                        SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 15);
                        for (var i = 0; i <= 360; i += 4)
                        {
                            var rad = Math.PI * i / 180;
                            var dX = (float) (10 * Math.Cos(rad));
                            var dY = (float) (10 * Math.Sin(rad));
                            var dust = Dust.NewDustPerfect(new Vector2(gauntlet.X + 10 * dX, gauntlet.Y + 10 * dY),
                                212,
                                new Vector2(dX * -1f + Player.velocity.X, dY * -1f + Player.velocity.Y), 0,
                                InfinityGauntlet.PowerColor);
                            dust.noGravity = true;
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < Player.GetModPlayer<ThanosPlayer>().powerStoneCharge / 10; i++)
                    {
                        var rad = Math.PI * Main.rand.NextDouble() * 2;
                        var dX = (float) (10 * Math.Cos(rad));
                        var dY = (float) (10 * Math.Sin(rad));

                        var dust = Dust.NewDustPerfect(new Vector2(gauntlet.X + 10 * dX, gauntlet.Y + 10 * dY),
                            212,
                            new Vector2(dX * -1f + Player.velocity.X, dY * -1f + Player.velocity.Y), 0,
                            InfinityGauntlet.PowerColor);
                        dust.noGravity = true;
                    }
                }
            }
            else if (DoomBubblesMod.powerStoneHotKey.JustReleased)
            {
                SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 74, 2f);
                SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 89, 2f);
                SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 93, .5f);
                Player.AddBuff(ModContent.BuffType<PowerStone>(),
                    6 * Player.GetModPlayer<ThanosPlayer>().powerStoneCharge);
                for (var i = 0; i <= 360; i += 5)
                {
                    var rad = Math.PI * i / 180;
                    var dX = (float) (20 * Math.Cos(rad));
                    var dY = (float) (20 * Math.Sin(rad));
                    Projectile.NewProjectile(new ProjectileSource_Item(Player, infinityGauntlet), gauntlet,
                        new Vector2(dX, dY), ModContent.ProjectileType<PowerExplosion>(),
                        (int) (Player.GetModPlayer<ThanosPlayer>().powerStoneCharge * 3.33333333f), 5,
                        Player.whoAmI);
                }


                Player.GetModPlayer<ThanosPlayer>().powerStoneCharge = 0;
            }
            else if (DoomBubblesMod.spaceStoneHotKey.JustPressed &&
                     !Player.HasBuff(ModContent.BuffType<SpaceStoneCooldown>()) &&
                     !Collision.SolidCollision(Main.MouseWorld, Player.width, Player.height) &&
                     Main.MouseWorld.X > 50f &&
                     Main.MouseWorld.X < Main.maxTilesX * 16 - 50 &&
                     Main.MouseWorld.Y > 50f &&
                     Main.MouseWorld.Y < Main.maxTilesY * 16 - 50)
            {
                var newPos = Main.MouseWorld;

                Projectile.NewProjectileDirect(new ProjectileSource_Item(Player, infinityGauntlet), Player.Center,
                    new Vector2(0, 0),
                    ModContent.ProjectileType<SpaceStoneWormhole>(), 0, 0, Player.whoAmI);
                for (var i = 0; i <= 360; i += 4)
                {
                    var rad = Math.PI * i / 180;
                    var dX = (float) (12 * Math.Cos(rad));
                    var dY = (float) (12 * Math.Sin(rad));
                    var dust = Dust.NewDustPerfect(new Vector2(Player.Center.X, Player.Center.Y), 212,
                        new Vector2(dX, dY), 0, InfinityGauntlet.SpaceColor, 1.5f);
                    dust.noGravity = true;
                }

                Player.Teleport(newPos, -1);

                Projectile.NewProjectileDirect(new ProjectileSource_Item(Player, infinityGauntlet), Player.Center,
                    new Vector2(0, 0),
                    ModContent.ProjectileType<SpaceStoneWormhole>(), 0, 0, Player.whoAmI);
                for (var i = 0; i <= 360; i += 4)
                {
                    var rad = Math.PI * i / 180;
                    var dX = (float) (12 * Math.Cos(rad));
                    var dY = (float) (12 * Math.Sin(rad));
                    var dust = Dust.NewDustPerfect(new Vector2(Player.Center.X, Player.Center.Y), 212,
                        new Vector2(dX, dY), 0, InfinityGauntlet.SpaceColor, 1.5f);
                    dust.noGravity = true;
                }

                Player.AddBuff(ModContent.BuffType<SpaceStoneCooldown>(), 360);
                SoundEngine.PlaySound(SoundID.Item8, Player.Center);
            }
            else if (DoomBubblesMod.realityStoneHotKey.Current)
            {
                if (Main.time % 15 == 0)
                {
                    SoundEngine.PlaySound(2, (int) Player.Center.X, (int) Player.Center.Y, 103);
                }

                var mousePos = Main.MouseWorld;
                var theta = Main.rand.NextDouble() * 2 * Math.PI;
                var x = mousePos.X + Main.rand.NextDouble() * 40 * Math.Cos(theta);
                var y = mousePos.Y + Main.rand.NextDouble() * 40 * Math.Sin(theta);
                Projectile.NewProjectile(new ProjectileSource_Item(Player, infinityGauntlet), (float) x, (float) y,
                    0f, 0f, ModContent.ProjectileType<RealityBeam>(), 100, 0,
                    Player.whoAmI);
            }
            else if (DoomBubblesMod.soulStoneHotKey.JustPressed)
            {
                if (Player.chest == -3)
                {
                    Player.chest = -1;
                    Recipe.FindRecipes();
                }
                else
                {
                    Player.chest = -3;
                    Player.chestX = (int) (Player.position.X / 16f);
                    Player.chestY = (int) (Player.position.Y / 16f);
                    Player.SetTalkNPC(-1);
                    Main.SetNPCShopIndex(0);
                    Main.playerInventory = true;
                    Main.recBigList = false;
                    Recipe.FindRecipes();
                    Player.GetModPlayer<ThanosPlayer>().soulStone = true;
                }
            }
            else if (DoomBubblesMod.timeStoneHotKey.JustPressed)
            {
                var previousHp = Player.GetModPlayer<ThanosPlayer>().timeHealth[300];
                if (previousHp > Player.statLife && !Player.HasBuff(ModContent.BuffType<TimeStoneCooldown>()))
                {
                    Player.HealEffect(previousHp - Player.statLife);
                    Player.statLife = previousHp;
                    for (var i = 0; i <= 360; i += 4)
                    {
                        var rad = Math.PI * i / 180;
                        var dX = (float) (5 * Math.Cos(rad));
                        var dY = (float) (5 * Math.Sin(rad));
                        var dust = Dust.NewDustPerfect(new Vector2(Player.Center.X, Player.Center.Y), 212,
                            new Vector2(dX, dY), 0, InfinityGauntlet.TimeColor);
                        dust.noGravity = true;
                    }

                    SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 15, 2f);
                    Player.AddBuff(ModContent.BuffType<TimeStoneCooldown>(), 1800);
                }
            }
            else if (DoomBubblesMod.mindStoneHotKey.JustPressed &&
                     !Player.HasBuff(ModContent.BuffType<MindStoneCooldown>()))
            {
                MindStone.MindAbility(Mod, Player);
            }
        }
    }

    public override void PreUpdate()
    {
        if (powerStone)
        {
            var buffIndex = Player.FindBuffIndex(ModContent.BuffType<PowerStone>());
            if (buffIndex != -1 && Player.buffTime[buffIndex] % 60 == 0)
            {
                foreach (var i in powerStoning)
                {
                    PowerStoneDamage(i, .1f);
                }
            }
        }

        if (soulStone) // TODO: DO SOUL STONE BETTER
        {
            Player.chestX = (int) (Player.position.X / 16f);
            Player.chestY = (int) (Player.position.Y / 16f);
            soulStoneTile = Main.tile[(int) (Player.position.X / 16f), (int) (Player.position.Y / 16f)];
            soulStoneTileActive = soulStoneTile.IsActive;
            if (!soulStoneTileActive)
            {
            }
        }


        for (var i = 300; i > 0; i--)
        {
            timeHealth[i] = timeHealth[i - 1];
        }

        timeHealth[0] = Player.statLife;

        base.PreUpdate();
    }

    public override void PostUpdate()
    {
        if (soulStone)
        {
            if (!soulStoneTileActive)
            {
            }
        }


        if (Player.chest != -3)
        {
            soulStone = false;
        }


        base.PostUpdate();
    }

    public override void UpdateDead()
    {
        powerStoning = new HashSet<int>();
    }

    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        if (Player.GetModPlayer<ThanosPlayer>().powerStone)
        {
            if (target.HasBuff(ModContent.BuffType<PowerStoneDebuff>()))
            {
                Player.GetModPlayer<ThanosPlayer>().PowerStoneDamage(target.whoAmI, .01f);
            }

            target.AddBuff(ModContent.BuffType<PowerStoneDebuff>(), 300);
            if (!Player.GetModPlayer<ThanosPlayer>().powerStoning.Contains(target.whoAmI))
            {
                Player.GetModPlayer<ThanosPlayer>().powerStoning.Add(target.whoAmI);
            }
        }
    }

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback,
        ref bool crit,
        ref int hitDirection)
    {
        if (Player.GetModPlayer<ThanosPlayer>().powerStone)
        {
            Player.GetModPlayer<ThanosPlayer>().PowerStoneDamage(target.whoAmI, .01f);
            target.AddBuff(ModContent.BuffType<PowerStoneDebuff>(), 300);
            if (!Player.GetModPlayer<ThanosPlayer>().powerStoning.Contains(target.whoAmI))
            {
                Player.GetModPlayer<ThanosPlayer>().powerStoning.Add(target.whoAmI);
            }
        }
    }

    private void PowerStoneDamage(int npcId, float multiplier)
    {
        var npc = Main.npc[npcId];
        var damage = 1000 + Math.Min((int) (npc.lifeMax * .001), 9000);
        damage = (int) (damage * multiplier);
        var knockback = 0f;
        var direction = 0;
        var crit = false;

        var combatText = -1;
        if (Main.netMode != NetmodeID.Server)
        {
            for (var i = 0; i < 100; i++)
            {
                if (Main.combatText[i].active)
                {
                    continue;
                }

                combatText = i;
                break;
            }
        }

        Player.ApplyDamageToNPC(npc, damage, knockback, direction, crit);
        Player.addDPS(damage);
        if (combatText != -1 && Main.combatText[combatText].active)
        {
            Main.combatText[combatText].color = InfinityGauntlet.PowerColor;
            Main.combatText[combatText].crit = multiplier > .05f;
            Main.combatText[combatText].dot = multiplier > .05f;
        }
    }
}