using System;
using System.Collections.Generic;
using System.Linq;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Content.Items.HotS;
using DoomBubblesMod.Content.Items.Misc;
using DoomBubblesMod.Content.Items.Talent;
using DoomBubblesMod.Content.Items.Weapons;
using Terraria.GameContent.ItemDropRules;

namespace DoomBubblesMod.Common.GlobalNPCs;

public class DoomBubblesGlobalNPC : GlobalNPC
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
            npc.buffImmune[BuffType<LivingBomb>()] = false;
        }
    }


    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        switch (npc.type)
        {
            case NPCID.DukeFishron:
                var fishronLoot = npcLoot.Get().OfType<LeadingConditionRule>().SelectMany(rule => rule.ChainedRules)
                    .OfType<Chains.TryIfSucceeded>().Select(succeeded => succeeded.RuleToChain)
                    .OfType<OneFromOptionsDropRule>()
                    .FirstOrDefault(rule => rule.dropIds.Contains(ItemID.RazorbladeTyphoon));

                if (fishronLoot != null)
                {
                    fishronLoot.dropIds = fishronLoot.dropIds.Append(ItemType<Ultrashark>()).ToArray();
                }
                else
                {
                    Mod.Logger.Warn("Failed to modify Duke Fishron Loot");
                }

                break;
            case NPCID.Mothron:
                npcLoot.Add(
                    new LeadingConditionRule(new Conditions.DownedAllMechBosses()).OnSuccess(
                        ItemDropRule.ExpertGetsRerolls(ItemType<BrokenHeroGun>(), 4, 1)));
                break;
        }

        /*if (Main.expertMode)
        {
            if (npc.type == NPCID.Mothron && Main.rand.Next(1, 3) == 1)
            {
                Item.NewItem(new EntitySource_Loot(npc), npc.position, ItemType<BrokenHeroGun>());
            }
        }
        else
        {
            switch (npc.type)
            {
                case NPCID.DukeFishron:
                    Item.NewItem(new EntitySource_Loot(npc), npc.position, ItemType<Ultrashark>());
                    break;
            }
        }*/
    }

    public override void SetupShop(int type, Chest shop, ref int nextSlot)
    {
        if (type == NPCID.Cyborg && NPC.downedPlantBoss)
        {
            var items = new List<ModItem>
            {
                GetInstance<LightningSurge>(), GetInstance<DiscordBlade>(),
                GetInstance<RepeaterCannon>(), GetInstance<PhaseBombLauncher>(),
                GetInstance<ShieldCapacitor>(),
                GetInstance<PylonStaff>(), GetInstance<PhotonCannonStaff>()
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
                GetInstance<FlamestrikeTome>(), GetInstance<LivingBombWand>(),
                GetInstance<VerdantSpheres>()
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