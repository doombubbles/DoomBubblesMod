using System.Linq;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Content.Items.Accessories;
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

        if (npc.FullName == "Hag") npc.GivenName = "Bitch";

        if (npc.boss) npc.buffImmune[BuffType<LivingBomb>()] = false;
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
                    fishronLoot.dropIds = fishronLoot.dropIds.Append(ItemType<Ultrashark>()).ToArray();
                else
                    Mod.Logger.Warn("Failed to modify Duke Fishron Loot");

                break;
            case NPCID.Mothron:
                npcLoot.Add(
                    new LeadingConditionRule(new Conditions.DownedAllMechBosses()).OnSuccess(
                        ItemDropRule.ExpertGetsRerolls(ItemType<BrokenHeroGun>(), 4, 1)));
                break;
            case NPCID.ShortBones:
            case NPCID.BigBoned:
            case NPCID.AngryBones:
            case NPCID.AngryBonesBig:
            case NPCID.AngryBonesBigMuscle:
            case NPCID.AngryBonesBigHelmet:
                npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<TriggerFinger>(), 100, 50));
                break;
        }
    }

    public override void ModifyShop(NPCShop shop)
    {
        foreach (var item in GetContent<ModItemWithTalents>())
        {
            if (item.SoldBy != shop.NpcType) continue;

            shop.Add(item.Item, Condition.DownedPlantera);
            shop.Add(item.Talent1Item.Item, Condition.DownedPumpking, Condition.DownedMourningWood);
            shop.Add(item.Talent2Item.Item, Condition.DownedIceQueen, Condition.DownedSantaNK1,
                Condition.DownedEverscream);
            shop.Add(item.Talent3Item.Item, Condition.DownedMartians);
        }

        switch (shop.NpcType)
        {
            case NPCID.DyeTrader:
                shop.Add<BlankDye>();
                break;
            case NPCID.Merchant:
                // shop.Add<SprayPaint>();
                break;
            case NPCID.WitchDoctor:
                // shop.Add<BloodlustTalisman>(Condition.InCrimson, Condition.DownedMoonLord);
                break;
        }
    }
}