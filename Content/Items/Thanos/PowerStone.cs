﻿using System;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Content.Projectiles.Thanos;
using DoomBubblesMod.Utils;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Items.Thanos;

internal class PowerStone : InfinityStone
{
    protected override int Rarity => ItemRarityID.Purple;
    protected override int Gem => ItemID.Amethyst;
    protected override Color Color => InfinityGauntlet.PowerColor;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Power Stone");
        Tooltip.SetDefault("\"The stone reacts to anything organic, the bigger\n" +
                           "the target, the bigger the power surge.\"\n" +
                           "-Gamora");

        Item.SetResearchAmount(1);
    }


    public static void PowerAbility(Player player, Item item)
    {
        var gauntletPos = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);

        if (player.channel)
        {
            player.itemAnimation = player.itemAnimationMax;
            PowerChargeUp(player, gauntletPos);
        }
        else
        {
            player.itemAnimation = 1;
            PowerRelease(player, item, gauntletPos);
        }
    }

    public static void PowerChargeUp(Player player, Vector2 gauntletPos)
    {
        if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
        {
            player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
            if (Main.time % 10 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item34, gauntletPos)
                    .Volume(.3f + .05f * Math.Sqrt(player.GetModPlayer<ThanosPlayer>().powerStoneCharge));
            }
        }

        if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge is > 100 and < 300)
        {
            player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
        }

        if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge is > 200 and < 300)
        {
            player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
        }

        if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge == 300)
        {
            if (Main.time % 10 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item15, gauntletPos);
                for (var i = 0; i <= 360; i += 4)
                {
                    var rad = Math.PI * i / 180;
                    var dX = (float) (10 * Math.Cos(rad));
                    var dY = (float) (10 * Math.Sin(rad));
                    var dust = Dust.NewDustPerfect(new Vector2(gauntletPos.X + 10 * dX, gauntletPos.Y + 10 * dY),
                        212,
                        new Vector2(dX * -1f + player.velocity.X, dY * -1f + player.velocity.Y), 0,
                        InfinityGauntlet.PowerColor);
                    dust.noGravity = true;
                }
            }
        }
        else
        {
            for (var i = 0; i < player.GetModPlayer<ThanosPlayer>().powerStoneCharge / 10; i++)
            {
                var rad = Math.PI * Main.rand.NextDouble() * 2;
                var dX = (float) (10 * Math.Cos(rad));
                var dY = (float) (10 * Math.Sin(rad));

                var dust = Dust.NewDustPerfect(new Vector2(gauntletPos.X + 10 * dX, gauntletPos.Y + 10 * dY),
                    212,
                    new Vector2(dX * -1f + player.velocity.X, dY * -1f + player.velocity.Y), 0,
                    InfinityGauntlet.PowerColor);
                dust.noGravity = true;
            }
        }
    }

    public static void PowerRelease(Player player, Item item, Vector2 gauntletPos)
    {
        SoundEngine.PlaySound(SoundID.Item74, gauntletPos).Volume(2f);
        SoundEngine.PlaySound(SoundID.Item89, gauntletPos).Volume(2f);
        SoundEngine.PlaySound(SoundID.Item93,gauntletPos).Volume(.5f);
        player.AddBuff(BuffType<PowerStoneBuff>(),
            6 * player.GetModPlayer<ThanosPlayer>().powerStoneCharge);
        for (var i = 0; i <= 360; i += 5)
        {
            var rad = Math.PI * i / 180;
            var dX = (float) (20 * Math.Cos(rad));
            var dY = (float) (20 * Math.Sin(rad));
            Projectile.NewProjectile(new EntitySource_ItemUse(player, item), gauntletPos,
                new Vector2(dX, dY), ProjectileType<PowerExplosion>(),
                (int) (player.GetModPlayer<ThanosPlayer>().powerStoneCharge * 3.33333333f), 5,
                player.whoAmI);
        }


        player.GetModPlayer<ThanosPlayer>().powerStoneCharge = 0;
    }
}