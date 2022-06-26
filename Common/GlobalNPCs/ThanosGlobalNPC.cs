using DoomBubblesMod.Content.Items.Thanos;

namespace DoomBubblesMod.Common.GlobalNPCs;

public class ThanosGlobalNPC : GlobalNPC
{
    public bool mindStoneFriendly;


    private int mindStoneNpcCount;
    private int mindStoneProjCount;
    public bool realityStoned;
    public override bool InstancePerEntity => true;


    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        if (mindStoneFriendly)
        {
            drawColor.B = (byte) (drawColor.B * .7f);
        }

        if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>().powerStoned)
        {
            drawColor.G = (byte) (drawColor.G * .7f);
            var dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.BubbleBurst_White,
                npc.velocity.X,
                npc.velocity.Y,
                100, InfinityGauntlet.PowerColor, 1.5f)];
            dust.noGravity = true;
        }
    }

    public override bool PreAI(NPC npc)
    {
        if (mindStoneFriendly)
        {
            var projCount = 0;
            var npcCount = 0;
            for (var i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active)
                {
                    projCount = i;
                }
                else
                {
                    break;
                }
            }

            for (var i = 0; i < 200; i++)
            {
                if (Main.npc[i].active)
                {
                    npcCount = i;
                }
                else
                {
                    break;
                }
            }

            mindStoneProjCount = projCount;
            mindStoneNpcCount = npcCount;
        }

        return base.PreAI(npc);
    }

    public override void PostAI(NPC npc)
    {
        if (mindStoneFriendly)
        {
            var projCount = 0;
            var npcCount = 0;
            for (var i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active)
                {
                    projCount = i;
                }
                else
                {
                    break;
                }
            }

            for (var i = 0; i < 200; i++)
            {
                if (Main.npc[i].active)
                {
                    npcCount = i;
                }
                else
                {
                    break;
                }
            }

            if (projCount > mindStoneProjCount)
            {
                for (var i = mindStoneProjCount + 1; i <= projCount; i++)
                {
                    var projectile = Main.projectile[i];
                    if (!projectile.friendly)
                    {
                        new MindStonePacket {Projectile = projectile}.HandleForAll();
                    }
                }
            }

            if (npcCount > mindStoneNpcCount)
            {
                for (var i = mindStoneNpcCount + 1; i <= npcCount; i++)
                {
                    var n = Main.npc[i];
                    if (!n.friendly && !n.boss)
                    {
                        new MindStonePacket {NPC = npc}.HandleForAll();
                    }
                }
            }
        }
    }
}