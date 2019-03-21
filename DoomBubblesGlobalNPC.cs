using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DoomBubblesMod.Items;
using DoomBubblesMod.Items.Thanos;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod
{
    class DoomBubblesGlobalNPC: GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public int Cleaved = 0;

        //public List<int> cleavedby = new List<int> { };

        public int frostmournedmg = 0;

        public int doomlightning;

        public bool mindStoneFriendly;
        
        
        
        public bool powerStoned;

        public int mindStoneProjCount;
        public int mindStoneNpcCount;

        public override void ResetEffects(NPC npc)
        {
            powerStoned = false;
            if (npc.FindBuffIndex(mod.BuffType("Cleaved")) == -1)
            {
                npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).Cleaved = 0;
            }
            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).doomlightning > 0)
            {
                npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).doomlightning -= 1;
            }
        }

        public override void SetDefaults(NPC npc)
        {
            if (!npc.boss)
            {
                npc.lifeMax = (int)(npc.lifeMax * DoomBubblesWorld.enemyHP);
                npc.life = (int)(npc.life * DoomBubblesWorld.enemyHP);
                npc.damage = (int)(npc.damage * DoomBubblesWorld.mobDMG);
            } else if (!npc.friendly)
            {
                npc.lifeMax = (int)(npc.lifeMax * DoomBubblesWorld.bossHP);
                npc.life = (int)(npc.life * DoomBubblesWorld.bossHP);
                npc.damage = (int)(npc.damage * DoomBubblesWorld.mobDMG);
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if(npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).Cleaved != 0)
            {
                drawColor.G = (byte)(drawColor.G * ((255f - (npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).Cleaved * 20f)) / 255f));
                drawColor.B = (byte)(drawColor.G * ((255f - (npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).Cleaved * 20f)) / 255f));
            }

            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).mindStoneFriendly)
            {
                drawColor.B = (byte) (drawColor.B * .7f);
            }

            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).powerStoned)
            {
                drawColor.G = (byte) (drawColor.G * .7f);
                Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 212, npc.velocity.X, npc.velocity.Y,
                    100, InfinityGauntlet.power, 1.5f)];
                dust.noGravity = true;

            }
        }


        public override bool PreAI(NPC npc)
        {
            
            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).mindStoneFriendly)
            {
                int projCount = 0;
                int npcCount = 0;
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active)
                    {
                        projCount = i;
                    }
                    else break;
                }
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active)
                    {
                        npcCount = i;
                    }
                    else break;
                }
                
                npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).mindStoneProjCount = projCount;
                npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).mindStoneNpcCount = npcCount;
            }
            
            return base.PreAI(npc);
        }

        public override void PostAI(NPC npc)
        {
            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).mindStoneFriendly)
            {
                int projCount = 0;
                int npcCount = 0;
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active)
                    {
                        projCount = i;
                    }
                    else break;
                }
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active)
                    {
                        npcCount = i;
                    }
                    else break;
                }

                if (projCount > npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).mindStoneProjCount)
                {
                    for (int i = npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).mindStoneProjCount + 1; i <= projCount; i++)
                    {
                        Projectile projectile = Main.projectile[i];
                        if (!projectile.friendly)
                        {
                            projectile.hostile = false;
                            if (Main.netMode == 1)
                            {
                                ModPacket packet = mod.GetPacket();
                                packet.Write((byte)DoomBubblesModMessageType.infinityStone);
                                packet.Write(projectile.whoAmI);
                                packet.Write(1);
                                packet.Send();
                            }
                        } 
                        
                    }
                }
                if (npcCount > npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).mindStoneNpcCount)
                {
                    for (int i = npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).mindStoneNpcCount + 1; i <= npcCount; i++)
                    {
                        NPC n = Main.npc[i];
                        if (!n.friendly && !n.boss)
                        {
                            n.damage = 0;
                            n.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).mindStoneFriendly = true;
                            if (Main.netMode == 1)
                            {
                                ModPacket packet = mod.GetPacket();
                                packet.Write((byte)DoomBubblesModMessageType.infinityStone);
                                packet.Write(n.whoAmI);
                                packet.Write(2);
                                packet.Send();
                            }
                        } 
                        
                    }
                }
            }
            base.PostAI(npc);
        }

        public override void NPCLoot(NPC npc)
        {
            //Main.NewText("Name: " + npc.FullName + ", Damage: " + npc.damage + ", Golem: " + NPC.downedGolemBoss + ", Day: " + Main.dayTime);
            //Spirit of Valoran Spawning
            if (npc.damage == 0 && Main.dayTime && !npc.SpawnedFromStatue && Main.rand.Next(4) == 1 && Math.Abs(Main.time - 27000) < 15000
                && NPC.downedGolemBoss && !Main.player[npc.target].ZoneMeteor && !Main.player[npc.target].ZoneGlowshroom && !Main.player[npc.target].ZoneDesert 
                && !Main.player[npc.target].ZoneJungle && !Main.player[npc.target].ZoneDungeon && !Main.player[npc.target].ZoneCorrupt && !Main.player[npc.target].ZoneCrimson 
                && !Main.player[npc.target].ZoneHoly && !Main.player[npc.target].ZoneSnow && !Main.player[npc.target].ZoneUndergroundDesert && !Main.player[npc.target].ZoneTowerNebula 
                && !Main.player[npc.target].ZoneTowerSolar && !Main.player[npc.target].ZoneTowerStardust && !Main.player[npc.target].ZoneTowerVortex 
                && Main.player[npc.target].ZoneOverworldHeight)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("ValoranSpirit"));
            }



            if (npc.type == NPCID.GoblinTinkerer)
            {
                Item.NewItem(npc.position, mod.ItemType("TinkerersCheatCodes"));
            }


            if (npc.type == NPCID.Mothron && Main.rand.Next(4) == 1)
            {
                Item.NewItem(npc.position, mod.ItemType("BrokenHeroGun"));
            }

            if (npc.type == NPCID.Plantera && !Main.expertMode)
            {
                Item.NewItem(npc.position, mod.ItemType("HeartOfTerraria"));
            }
            
            

            //Frostmourne Spirits
            int frederick = npc.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).frostmournedmg;

            while (frederick > 1000)
            {
                Item.NewItem((int)npc.Center.X, (int)npc.Center.Y, npc.width, npc.height, mod.ItemType("Spirit"));
                frederick -= 1000;
            }
            if (Main.rand.Next(1, 1000) <= frederick)
            {
                Item.NewItem((int)npc.Center.X, (int)npc.Center.Y, npc.width, npc.height, mod.ItemType("Spirit"));
            }



        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            switch (type)
            {
                case NPCID.DyeTrader:
                    if (Main.hardMode)
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("BlankDye"));
                        nextSlot++;
                    }
                    break;
            }
            base.SetupShop(type, shop, ref nextSlot);
        }
    }
}
