using System;
using DoomBubblesMod.Common.GlobalNPCs;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Utils;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Items.Thanos;

public class MindStone : InfinityStone
{
    protected override int Rarity => ItemRarityID.Yellow;
    protected override int Gem => ItemID.Topaz;
    protected override Color Color => InfinityGauntlet.MindColor;

    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("\"The Mind Stone is the fourth of the Infinity Stones to show up in the last\n" +
                           "few years. It's not a coincidence. Someone has been playing an intricate\n" +
                           "game and has made pawns of us.\"\n" +
                           "-Thor");
        SacrificeTotal = 1;
    }

    public static void MindAbility(Mod mod, Player player)
    {
        var newPos = Main.MouseWorld;

        for (var i = 0; i <= 360; i += 3)
        {
            var rad = Math.PI * i / 180;
            var dX = (float) (9 * Math.Cos(rad));
            var dY = (float) (9 * Math.Sin(rad));
            var dust = Dust.NewDustPerfect(newPos, 212, new Vector2(dX, dY), 0, InfinityGauntlet.MindColor, 1.5f);
            dust.noGravity = true;
        }

        for (var i = 0; i <= 360; i += 3)
        {
            var rad = Math.PI * i / 180;
            var dX = (float) (9 * Math.Cos(rad));
            var dY = (float) (9 * Math.Sin(rad));
            var dust = Dust.NewDustPerfect(new Vector2(newPos.X + 11 * dX, newPos.Y + 11 * dY), 212,
                new Vector2(-dX, -dY), 0, InfinityGauntlet.MindColor, 1.5f);
            dust.noGravity = true;
        }

        SoundEngine.PlaySound(mod.Sound("MindStone"), player.position);

        foreach (var npc in Main.npc)
        {
            if (npc.Distance(newPos) < 100f && !npc.friendly && npc.TypeName != "Target Dummy" && !npc.boss)
            {
                npc.damage = 0;

                new MindStonePacket {NPC = npc}.HandleForAll();
            }
        }


        player.AddBuff(BuffType<MindStoneCooldown>(), 1200);
    }
}

public class MindStonePacket : CustomPacket<MindStonePacket>
{
    public NPC NPC { get; set; }

    public Projectile Projectile { get; set; }

    protected override void SetDefaults()
    {
        NPC = null;
        Projectile = null;
    }

    public override void HandlePacket()
    {
        if (NPC != null)
        {
            NPC.GetGlobalNPC<ThanosGlobalNPC>().mindStoneFriendly = true;
            NPC.damage = 0;
        }

        if (Projectile != null)
        {
            Projectile.hostile = false;
        }
    }
}