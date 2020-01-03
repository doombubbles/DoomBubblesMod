using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DoomBubblesMod.Items;
using DoomBubblesMod.Items.Thanos;
using DoomBubblesMod.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod
{
    class DoomBubblesGlobalNPC: GlobalNPC
    {
        public override bool InstancePerEntity => true;

        //public List<int> cleavedby = new List<int> { };

        public bool mindStoneFriendly;
        
        
        
        public bool powerStoned;

        public int mindStoneProjCount;
        public int mindStoneNpcCount;

        public override void ResetEffects(NPC npc)
        {
            powerStoned = false;

            if (npc.FullName == "Hag")
            {
                npc.GivenName = "Bitch";
            }

            if (npc.boss)
            {
                npc.buffImmune[mod.BuffType("LivingBomb")] = false;
            }
        }
        
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if(npc.HasBuff(mod.BuffType("Cleaved")))
            {
                int stacks = 1 + npc.buffTime[npc.FindBuffIndex(mod.BuffType("Cleaved"))] % 10;
                drawColor.G = (byte)(drawColor.G * ((255f - (stacks * 15)) / 255f));
                drawColor.B = (byte)(drawColor.G * ((255f - (stacks * 15)) / 255f));
            }

            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly)
            {
                drawColor.B = (byte) (drawColor.B * .7f);
            }

            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>().powerStoned)
            {
                drawColor.G = (byte) (drawColor.G * .7f);
                Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 212, npc.velocity.X, npc.velocity.Y,
                    100, InfinityGauntlet.power, 1.5f)];
                dust.noGravity = true;

            }
        }


        public override bool PreAI(NPC npc)
        {
            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly)
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
                
                npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneProjCount = projCount;
                npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneNpcCount = npcCount;
            }
            return base.PreAI(npc);
        }

        public override void PostAI(NPC npc)
        {
            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly)
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

                if (projCount > npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneProjCount)
                {
                    for (int i = npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneProjCount + 1; i <= projCount; i++)
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
                if (npcCount > npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneNpcCount)
                {
                    for (int i = npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneNpcCount + 1; i <= npcCount; i++)
                    {
                        NPC n = Main.npc[i];
                        if (!n.friendly && !n.boss)
                        {
                            n.damage = 0;
                            n.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly = true;
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
        }

        public override void NPCLoot(NPC npc)
        {
            if (Main.expertMode)
            {
                if (npc.type == NPCID.Mothron && Main.rand.Next(1,3) == 1)
                {
                    Item.NewItem(npc.position, mod.ItemType("BrokenHeroGun"));
                }
            }
            else
            {
                if (npc.type == NPCID.Mothron && Main.rand.Next(1,4) == 1)
                {
                    Item.NewItem(npc.position, mod.ItemType("BrokenHeroGun"));
                }
                if (npc.type == NPCID.Plantera)
                {
                    Item.NewItem(npc.position, mod.ItemType("HeartOfTerraria"));
                }

                if (npc.type == NPCID.DukeFishron && Main.rand.Next(1, 5) == 1)
                {
                    Item.NewItem(npc.position, mod.ItemType("Ultrashark"));
                
                }
            }

            if (npc.boss)
            {
                for (var i = 0; i < Main.player.Length; i++)
                {
                    var player1 = Main.player[i];
                    if (player1.active && player1.GetModPlayer<LoLPlayer>().DarkHarvest)
                    {
                        player1.GetModPlayer<LoLPlayer>().keystoneCooldown = 90;
                    }

                }

            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Cyborg && NPC.downedPlantBoss)
            {
                List<ModItem> items = new List<ModItem>{mod.GetItem("LightningSurge"), mod.GetItem("DiscordBlade"),
                    mod.GetItem("RepeaterCannon"), mod.GetItem("PhaseBombLauncher"), mod.GetItem("ShieldCapacitor"),
                    mod.GetItem("PylonStaff"), mod.GetItem("PhotonCannonStaff")
                };
                
                foreach (var modItem in items)
                {
                    shop.item[nextSlot].SetDefaults(modItem.item.type);
                    nextSlot++;
                }

                var hash = Math.Abs(Main.LocalPlayer.name.GetHashCode());
                
                foreach (var modItem in items)
                {
                    TalentItem talentItem = (TalentItem) modItem;

                    if (Main.LocalPlayer.HasItem(talentItem.item.type))
                    {
                        if (NPC.downedHalloweenKing && NPC.downedHalloweenTree)
                        {
                            addTalent(talentItem, hash % 3 + 1, shop, ref nextSlot);
                        }
                        if (NPC.downedChristmasIceQueen && NPC.downedChristmasSantank && NPC.downedChristmasTree)
                        {
                            addTalent(talentItem, (hash + 1) % 3 + 1, shop, ref nextSlot);
                        }
                        if (NPC.downedMartians)
                        {
                            addTalent(talentItem, (hash + 2) % 3 + 1, shop, ref nextSlot);
                        }
                    }
                }

            }
            
            if (type == NPCID.Wizard && NPC.downedPlantBoss)
            {
                List<ModItem> items = new List<ModItem>{mod.GetItem("FlamestrikeTome"), mod.GetItem("LivingBombWand"),
                    mod.GetItem("VerdantSpheres")
                };
                
                foreach (var modItem in items)
                {
                    shop.item[nextSlot].SetDefaults(modItem.item.type);
                    nextSlot++;
                }

                var hash = Math.Abs(Main.LocalPlayer.name.GetHashCode());
                
                foreach (var modItem in items)
                {
                    TalentItem talentItem = (TalentItem) modItem;

                    if (Main.LocalPlayer.HasItem(talentItem.item.type))
                    {
                        if (NPC.downedHalloweenKing && NPC.downedHalloweenTree)
                        {
                            addTalent(talentItem, hash % 3 + 1, shop, ref nextSlot);
                        }
                        if (NPC.downedChristmasIceQueen && NPC.downedChristmasSantank && NPC.downedChristmasTree)
                        {
                            addTalent(talentItem, (hash + 1) % 3 + 1, shop, ref nextSlot);
                        }
                        if (NPC.downedMartians)
                        {
                            addTalent(talentItem, (hash + 2) % 3 + 1, shop, ref nextSlot);
                        }
                    }
                }

            }


            /*
            if (type == NPCID.WitchDoctor && Main.LocalPlayer.ZoneCrimson && NPC.downedMoonlord)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("BloodlustTalisman"));
                nextSlot++;
            }
            */
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (npc.HasBuff(mod.BuffType("Exposed")))
            {
                damage = (int) (damage * 1.1);
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            if (npc.HasBuff(mod.BuffType("Exposed")))
            {
                damage = (int) (damage * 1.1);
            }
        }

        private void addTalent(TalentItem talentItem, int i, Chest shop, ref int nextSlot)
        {
            switch (i)
            {
                case 1:
                    shop.item[nextSlot].SetDefaults(mod.ItemType(talentItem.Talent1Name));
                    nextSlot++;
                    break;
                case 2:
                    shop.item[nextSlot].SetDefaults(mod.ItemType(talentItem.Talent2Name));
                    nextSlot++;
                    break;
                case 3:
                    shop.item[nextSlot].SetDefaults(mod.ItemType(talentItem.Talent3Name));
                    nextSlot++;
                    break;
            }
        }
    }
}
