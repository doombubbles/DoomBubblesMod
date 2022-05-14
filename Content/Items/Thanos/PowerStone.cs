using System;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Projectiles.Thanos;
using DoomBubblesMod.Utils;
using Terraria.Audio;
using Terraria.ID;

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
        var gauntlet = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);

        if (player.channel)
        {
            player.itemAnimation = player.itemAnimationMax;
            if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
            {
                player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                if (Main.time % 10 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item, (int) gauntlet.X, (int) gauntlet.Y, 34,
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
                    SoundEngine.PlaySound(SoundID.Item, (int) gauntlet.X, (int) gauntlet.Y, 15);
                    for (var i = 0; i <= 360; i += 4)
                    {
                        var rad = Math.PI * i / 180;
                        var dX = (float) (10 * Math.Cos(rad));
                        var dY = (float) (10 * Math.Sin(rad));
                        var dust = Dust.NewDustPerfect(new Vector2(gauntlet.X + 10 * dX, gauntlet.Y + 10 * dY), 212,
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

                    var dust = Dust.NewDustPerfect(new Vector2(gauntlet.X + 10 * dX, gauntlet.Y + 10 * dY), 212,
                        new Vector2(dX * -1f + player.velocity.X, dY * -1f + player.velocity.Y), 0,
                        InfinityGauntlet.PowerColor);
                    dust.noGravity = true;
                }
            }
        }
        else
        {
            SoundEngine.PlaySound(SoundID.Item, (int) gauntlet.X, (int) gauntlet.Y, 74, 2f);
            SoundEngine.PlaySound(SoundID.Item, (int) gauntlet.X, (int) gauntlet.Y, 89, 2f);
            SoundEngine.PlaySound(SoundID.Item, (int) gauntlet.X, (int) gauntlet.Y, 93, .5f);
            player.itemAnimation = 1;

            player.AddBuff(BuffType<Buffs.PowerStone>(),
                6 * player.GetModPlayer<ThanosPlayer>().powerStoneCharge);
            for (var i = 0; i <= 360; i += 5)
            {
                var rad = Math.PI * i / 180;
                var dX = (float) (20 * Math.Cos(rad));
                var dY = (float) (20 * Math.Sin(rad));
                Projectile.NewProjectile(new EntitySource_ItemUse(player, item), gauntlet, new Vector2(dX, dY),
                    ProjectileType<PowerExplosion>(),
                    (int) (player.GetModPlayer<ThanosPlayer>().powerStoneCharge * 3.33333333f), 5, player.whoAmI);
            }

            player.GetModPlayer<ThanosPlayer>().powerStoneCharge = 0;
        }
    }
}