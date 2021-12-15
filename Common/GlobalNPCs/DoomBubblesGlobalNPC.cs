using System;
using System.Collections.Generic;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Content.Items.HotS;
using DoomBubblesMod.Content.Items.Misc;
using DoomBubblesMod.Content.Items.Talent;
using DoomBubblesMod.Content.Items.Weapons;

namespace DoomBubblesMod.Common.GlobalNPCs;

internal class DoomBubblesGlobalNPC : GlobalNPC
{
    //public List<int> cleavedby = new List<int> { };

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
            switch (npc.type)
            {
                case NPCID.Mothron when Main.rand.Next(1, 4) == 1:
                    Item.NewItem(npc.position, ModContent.ItemType<BrokenHeroGun>());
                    break;
                case NPCID.Plantera:
                    //Item.NewItem(npc.position, ModContent.ItemType<HeartOfTerraria>());
                    break;
                case NPCID.DukeFishron when Main.rand.Next(1, 5) == 1:
                    Item.NewItem(npc.position, ModContent.ItemType<Ultrashark>());
                    break;
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
                ModContent.GetInstance<RepeaterCannon>(), ModContent.GetInstance<PhaseBombLauncher>(),
                ModContent.GetInstance<ShieldCapacitor>(),
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
                var talentItem = (ModItemWithTalents) modItem;

                if (Main.LocalPlayer.HasItem(talentItem.Item.type))
                {
                    if (NPC.downedHalloweenKing && NPC.downedHalloweenTree)
                    {
                        AddTalent(talentItem, hash % 3 + 1, shop, ref nextSlot);
                    }

                    if (NPC.downedChristmasIceQueen && NPC.downedChristmasSantank && NPC.downedChristmasTree)
                    {
                        AddTalent(talentItem, (hash + 1) % 3 + 1, shop, ref nextSlot);
                    }

                    if (NPC.downedMartians)
                    {
                        AddTalent(talentItem, (hash + 2) % 3 + 1, shop, ref nextSlot);
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
                var talentItem = (ModItemWithTalents) modItem;

                if (Main.LocalPlayer.HasItem(talentItem.Item.type))
                {
                    if (NPC.downedHalloweenKing && NPC.downedHalloweenTree)
                    {
                        AddTalent(talentItem, hash % 3 + 1, shop, ref nextSlot);
                    }

                    if (NPC.downedChristmasIceQueen && NPC.downedChristmasSantank && NPC.downedChristmasTree)
                    {
                        AddTalent(talentItem, (hash + 1) % 3 + 1, shop, ref nextSlot);
                    }

                    if (NPC.downedMartians)
                    {
                        AddTalent(talentItem, (hash + 2) % 3 + 1, shop, ref nextSlot);
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

    private static void AddTalent(ModItemWithTalents modItemWithTalents, int i, Chest shop, ref int nextSlot)
    {
        switch (i)
        {
            case 1:
                shop.item[nextSlot].SetDefaults(modItemWithTalents.Talent1Item.Type);
                nextSlot++;
                break;
            case 2:
                shop.item[nextSlot].SetDefaults(modItemWithTalents.Talent1Item.Type);
                nextSlot++;
                break;
            case 3:
                shop.item[nextSlot].SetDefaults(modItemWithTalents.Talent3Item.Type);
                nextSlot++;
                break;
        }
    }
}