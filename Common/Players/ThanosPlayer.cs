using System;
using System.Collections.Generic;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Content.Items.Thanos;
using Terraria;
using Terraria.GameInput;

namespace DoomBubblesMod.Common.Players;

public class ThanosPlayer : ModPlayer
{
    public int gem = -1;

    public Item infinityGauntlet;
    public bool powerStone;
    public int powerStoneCharge;
    public int powerStoned;
    public HashSet<int> powerStoning = [];
    public bool soulStone;
    public Tile soulStoneTile;
    public bool soulStoneTileActive;
    public int[] timeHealth = new int[302];

    public override void ResetEffects()
    {
        powerStone = false;
        if (powerStoned > 0)
        {
            powerStoned--;
        }

        powerStoning.RemoveWhere(i => !Main.npc[i].HasBuff<PowerStoneDebuff>());
        infinityGauntlet = null;
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        base.ProcessTriggers(triggersSet);
        var gauntletPos = new Vector2(Player.Center.X + 10 * Player.direction, Player.Center.Y - 25);

        if (infinityGauntlet != null && Player.itemTime == 0 && Player.itemAnimation == 0)
        {
            if (PowerStoneHotKey.Current)
            {
                PowerStone.PowerChargeUp(Player, gauntletPos);
            }
            else if (PowerStoneHotKey.JustReleased)
            {
                PowerStone.PowerRelease(Player, infinityGauntlet, gauntletPos);
            }
            else if (SpaceStoneHotKey.JustPressed &&
                     !Player.HasBuff<SpaceStoneCooldown>() &&
                     !Collision.SolidCollision(Main.MouseWorld, Player.width, Player.height) &&
                     Main.MouseWorld.X > 50f &&
                     Main.MouseWorld.X < Main.maxTilesX * 16 - 50 &&
                     Main.MouseWorld.Y > 50f &&
                     Main.MouseWorld.Y < Main.maxTilesY * 16 - 50)
            {
                SpaceStone.SpaceAbility(Player, infinityGauntlet);
            }
            else if (RealityStoneHotKey.Current)
            {
                RealityStone.RealityChannel(Player, infinityGauntlet);
            }
            else if (SoulStoneHotKey.JustPressed)
            {
                SoulStone.SoulAbility(Player);
            }
            else if (TimeStoneHotKey.JustPressed)
            {
                TimeStone.TimeAbility(Player);
            }
            else if (MindStoneHotKey.JustPressed &&
                     !Player.HasBuff<MindStoneCooldown>())
            {
                MindStone.MindAbility(Mod, Player);
            }
        }
    }

    public override void PreUpdate()
    {
        if (powerStone)
        {
            var buffIndex = Player.FindBuffIndex(BuffType<PowerStoneBuff>());
            if (buffIndex != -1 && Player.buffTime[buffIndex] % 60 == 0)
            {
                foreach (var i in powerStoning)
                {
                    PowerStoneDamage(i, .1f);
                }
            }
        }

        for (var i = 300; i > 0; i--)
        {
            timeHealth[i] = timeHealth[i - 1];
        }

        timeHealth[0] = Player.statLife;

        base.PreUpdate();
    }

    public override void UpdateDead()
    {
        powerStoning = [];
    }

    public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Item, consider using ModifyHitNPC instead */
    {
        if (Player.GetModPlayer<ThanosPlayer>().powerStone)
        {
            if (target.HasBuff<PowerStoneDebuff>())
            {
                Player.GetModPlayer<ThanosPlayer>().PowerStoneDamage(target.whoAmI, .01f);
            }

            target.AddBuff(BuffType<PowerStoneDebuff>(), 300);
            if (!Player.GetModPlayer<ThanosPlayer>().powerStoning.Contains(target.whoAmI))
            {
                Player.GetModPlayer<ThanosPlayer>().powerStoning.Add(target.whoAmI);
            }
        }
    }

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
    {
        if (Player.GetModPlayer<ThanosPlayer>().powerStone)
        {
            Player.GetModPlayer<ThanosPlayer>().PowerStoneDamage(target.whoAmI, .01f);
            target.AddBuff(BuffType<PowerStoneDebuff>(), 300);
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