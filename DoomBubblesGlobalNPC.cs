using System;
using System.Collections.Generic;
using DoomBubblesMod.Buffs;
using DoomBubblesMod.Items;
using DoomBubblesMod.Items.HotS;
using DoomBubblesMod.Items.Talent;
using DoomBubblesMod.Items.Thanos;
using DoomBubblesMod.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod
{
    internal class DoomBubblesGlobalNPC : GlobalNPC
    {
        //public List<int> cleavedby = new List<int> { };

        public bool mindStoneFriendly;
        public int mindStoneNpcCount;

        public int mindStoneProjCount;

        public bool powerStoned;
        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            powerStoned = false;

            if (npc.FullName == "Hag")
            {
                npc.GivenName = "Bitch";
            }

            if (npc.boss)
            {
                npc.buffImmune[ModContent.BuffType<LivingBomb>()] = false;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly)
            {
                drawColor.B = (byte) (drawColor.B * .7f);
            }

            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>().powerStoned)
            {
                drawColor.G = (byte) (drawColor.G * .7f);
                var dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.BubbleBurst_White, npc.velocity.X,
                    npc.velocity.Y,
                    100, InfinityGauntlet.power, 1.5f)];
                dust.noGravity = true;
            }
        }


        public override bool PreAI(NPC npc)
        {
            if (npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly)
            {
                var projCount = 0;
                var npcCount = 0;
                for (var i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active)
                    {
                        projCount = i;
                    }
                    else break;
                }

                for (var i = 0; i < 200; i++)
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
                var projCount = 0;
                var npcCount = 0;
                for (var i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active)
                    {
                        projCount = i;
                    }
                    else break;
                }

                for (var i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active)
                    {
                        npcCount = i;
                    }
                    else break;
                }

                if (projCount > npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneProjCount)
                {
                    for (var i = npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneProjCount + 1; i <= projCount; i++)
                    {
                        var projectile = Main.projectile[i];
                        if (!projectile.friendly)
                        {
                            projectile.hostile = false;
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte) DoomBubblesModMessageType.infinityStone);
                                packet.Write(projectile.whoAmI);
                                packet.Write(1);
                                packet.Send();
                            }
                        }
                    }
                }

                if (npcCount > npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneNpcCount)
                {
                    for (var i = npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneNpcCount + 1; i <= npcCount; i++)
                    {
                        var n = Main.npc[i];
                        if (!n.friendly && !n.boss)
                        {
                            n.damage = 0;
                            n.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly = true;
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte) DoomBubblesModMessageType.infinityStone);
                                packet.Write(n.whoAmI);
                                packet.Write(2);
                                packet.Send();
                            }
                        }
                    }
                }
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (Main.expertMode)
            {
                if (npc.type == NPCID.Mothron && Main.rand.Next(1, 3) == 1)
                {
                    Item.NewItem(npc.position, ModContent.ItemType<BrokenHeroGun>());
                }
            }
            else
            {
                if (npc.type == NPCID.Mothron && Main.rand.Next(1, 4) == 1)
                {
                    Item.NewItem(npc.position, ModContent.ItemType<BrokenHeroGun>());
                }

                if (npc.type == NPCID.Plantera)
                {
                    //Item.NewItem(npc.position, ModContent.ItemType<HeartOfTerraria>());
                }

                if (npc.type == NPCID.DukeFishron && Main.rand.Next(1, 5) == 1)
                {
                    Item.NewItem(npc.position, ModContent.ItemType<Ultrashark>());
                }
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Cyborg && NPC.downedPlantBoss)
            {
                var items = new List<ModItem>
                {
                    ModContent.GetInstance<LightningSurge>(), ModContent.GetInstance<DiscordBlade>(),
                    ModContent.GetInstance<RepeaterCannon>(), ModContent.GetInstance<PhaseBombLauncher>(), ModContent.GetInstance<ShieldCapacitor>(),
                    ModContent.GetInstance<PylonStaff>(), ModContent.GetInstance<PhotonCannonStaff>()
                };

                foreach (var modItem in items)
                {
                    shop.item[nextSlot].SetDefaults(modItem.Item.type);
                    nextSlot++;
                }

                var hash = Math.Abs(Main.LocalPlayer.name.GetHashCode());

                foreach (var modItem in items)
                {
                    var talentItem = (TalentItem) modItem;

                    if (Main.LocalPlayer.HasItem(talentItem.Item.type))
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
                var items = new List<ModItem>
                {
                    ModContent.GetInstance<FlamestrikeTome>(), ModContent.GetInstance<LivingBombWand>(),
                    ModContent.GetInstance<VerdantSpheres>()
                };

                foreach (var modItem in items)
                {
                    shop.item[nextSlot].SetDefaults(modItem.Item.type);
                    nextSlot++;
                }

                var hash = Math.Abs(Main.LocalPlayer.name.GetHashCode());

                foreach (var modItem in items)
                {
                    var talentItem = (TalentItem) modItem;

                    if (Main.LocalPlayer.HasItem(talentItem.Item.type))
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
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<BloodlustTalisman>());
                nextSlot++;
            }
            */
        }

        private void addTalent(TalentItem talentItem, int i, Chest shop, ref int nextSlot)
        {
            switch (i)
            {
                case 1:
                    shop.item[nextSlot].SetDefaults(ModContent.Find<ModItem>(talentItem.Talent1Name).Type);
                    nextSlot++;
                    break;
                case 2:
                    shop.item[nextSlot].SetDefaults(ModContent.Find<ModItem>(talentItem.Talent2Name).Type);
                    nextSlot++;
                    break;
                case 3:
                    shop.item[nextSlot].SetDefaults(ModContent.Find<ModItem>(talentItem.Talent3Name).Type);
                    nextSlot++;
                    break;
            }
        }
    }
}