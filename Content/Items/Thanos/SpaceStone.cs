using System;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Content.Projectiles.Thanos;
using DoomBubblesMod.Utils;
using Terraria.Audio;
using Terraria.DataStructures;

namespace DoomBubblesMod.Content.Items.Thanos;

internal class SpaceStone : InfinityStone
{
    protected override int Rarity => ItemRarityID.Blue;
    protected override int Gem => ItemID.Sapphire;
    protected override Color Color => InfinityGauntlet.SpaceColor;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Space Stone");
        Tooltip.SetDefault("\"A lifetime ago, I too sought the stones. I even held one in my hand.\"\n" +
                           "-Red Skull to Thanos");
        Item.SetResearchAmount(1);
    }
    
    public static void SpaceAbility(Player player, Item item)
    {
        var newPos = Main.MouseWorld;

        Projectile.NewProjectileDirect(new ProjectileSource_Item(player, item), player.Center, new Vector2(0, 0),
            ModContent.ProjectileType<SpaceStoneWormhole>(),
            0, 0, player.whoAmI);
        for (var i = 0; i <= 360; i += 4)
        {
            var rad = Math.PI * i / 180;
            var dX = (float) (12 * Math.Cos(rad));
            var dY = (float) (12 * Math.Sin(rad));
            var dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY),
                0, InfinityGauntlet.SpaceColor, 1.5f);
            dust.noGravity = true;
        }

        player.Teleport(newPos, -1);

        Projectile.NewProjectileDirect(new ProjectileSource_Item(player, item), player.Center, new Vector2(0, 0),
            ModContent.ProjectileType<SpaceStoneWormhole>(),
            0, 0, player.whoAmI);
        for (var i = 0; i <= 360; i += 4)
        {
            var rad = Math.PI * i / 180;
            var dX = (float) (12 * Math.Cos(rad));
            var dY = (float) (12 * Math.Sin(rad));
            var dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY),
                0, InfinityGauntlet.SpaceColor, 1.5f);
            dust.noGravity = true;
        }

        player.AddBuff(ModContent.BuffType<SpaceStoneCooldown>(), 360);
        SoundEngine.PlaySound(SoundID.Item8, player.Center);
    }
}