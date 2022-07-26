using System;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Utils;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Items.Thanos;

public class TimeStone : InfinityStone
{
    protected override int Rarity => ItemRarityID.Green;
    protected override int Gem => ItemID.Emerald;
    protected override Color Color => InfinityGauntlet.TimeColor;

    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("\"Dormammu, I've come to bargain.\"\n" +
                           "-Doctor Strange, multiple occasions");
        SacrificeTotal = 1;
    }

    public static void TimeAbility(Player player)
    {
        var gauntletPos = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);
        var previousHp = player.GetModPlayer<ThanosPlayer>().timeHealth[300];

        if (previousHp > player.statLife && !player.HasBuff<TimeStoneCooldown>())
        {
            player.HealEffect(previousHp - player.statLife);
            player.statLife = previousHp;
            for (var i = 0; i <= 360; i += 4)
            {
                var rad = Math.PI * i / 180;
                var dX = (float) (5 * Math.Cos(rad));
                var dY = (float) (5 * Math.Sin(rad));
                var dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY),
                    0, InfinityGauntlet.TimeColor);
                dust.noGravity = true;
            }

            SoundEngine.PlaySound(SoundID.Item15.WithVolumeScale(2f), gauntletPos);
            player.AddBuff(BuffType<TimeStoneCooldown>(), 1800);
        }
    }
}