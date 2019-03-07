using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.NPCs
{
	public class ValoranSpirit : ModNPC
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of Valoran");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.DungeonSpirit];
        }

		public override void SetDefaults()
		{
			npc.width = 40;
			npc.height = 40;
			npc.damage = 50;
			npc.defense = 35;
			npc.lifeMax = 400;
			npc.HitSound = SoundID.NPCHit5;
			npc.DeathSound = SoundID.NPCDeath7;
			npc.value = 10000f;
			npc.knockBackResist = 0.5f;
            npc.noGravity = true;
            npc.noTileCollide = true;
			npc.aiStyle = -1;
			animationType = 288;
            
		}

        public override void AI()
        {
            npc.TargetClosest(true);
            float num874 = Main.player[npc.target].Center.X - npc.position.X;
            float num875 = Main.player[npc.target].Center.Y - npc.position.Y;
            float num876 = (float)System.Math.Sqrt((double)(num874 * num874 + num875 * num875));
            float num877 = 12f;
            num876 = num877 / num876;
            num874 *= num876;
            num875 *= num876;
            npc.velocity.X = (npc.velocity.X * 100f + num874) / 101f;
            npc.velocity.Y = (npc.velocity.Y * 100f + num875) / 101f;
            npc.rotation = (float)System.Math.Atan2((double)num875, (double)num874) - 1.57f;
            int num878 = Dust.NewDust(npc.position, npc.width, npc.height, 61, 0f, 0f, 0);
            Main.dust[num878].velocity *= 0.1f;
            Main.dust[num878].scale = 1.3f;
            Main.dust[num878].noGravity = true;
            return;
        }

        public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
                int dustType = 61;
				int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale = 1f;
			}
		}

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RunicEssence"), Main.rand.Next(1) + 1);
        }
    }
}
