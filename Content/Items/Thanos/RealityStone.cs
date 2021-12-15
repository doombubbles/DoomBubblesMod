using System;
using DoomBubblesMod.Content.Projectiles.Thanos;
using DoomBubblesMod.Utils;
using Terraria.Audio;
using Terraria.DataStructures;

namespace DoomBubblesMod.Content.Items.Thanos;

internal class RealityStone : InfinityStone
{
    protected override int Rarity => ItemRarityID.Red;
    protected override int Gem => ItemID.Ruby;
    protected override Color Color => InfinityGauntlet.RealityColor;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Reality Stone");
        Tooltip.SetDefault("\"Long before the birth of light there was darkness, and from that darkness\n" +
                           "came the Dark Elves. Millennia ago the most ruthless of their\n" +
                           "kind, Malekith, sought to transform our universe back into one of eternal\n" +
                           "night. Such evil was possible through the power of the Aether, an ancient\n" +
                           "force of infinite destruction.\"\n" +
                           "-Odin");
        Item.SetResearchAmount(1);
    }

    public static void RealityAbility(Player player, Item item)
    {
        if (player.channel)
        {
            if (Main.time % 15 == 0)
            {
                SoundEngine.PlaySound(2, (int) player.Center.X, (int) player.Center.Y, 103);
            }

            player.itemAnimation = player.itemAnimationMax;

            var mousePos = Main.MouseWorld;
            var theta = Main.rand.NextDouble() * 2 * Math.PI;
            var x = mousePos.X + Main.rand.NextDouble() * 40 * Math.Cos(theta);
            var y = mousePos.Y + Main.rand.NextDouble() * 40 * Math.Sin(theta);
            Projectile.NewProjectile(new ProjectileSource_Item(player, item), (float) x, (float) y, 0f, 0f,
                ModContent.ProjectileType<RealityBeam>(), 100, 0,
                player.whoAmI);
        }
        else
        {
            player.itemAnimation = 1;
        }
    }
}