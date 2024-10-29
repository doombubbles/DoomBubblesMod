using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Talent;

namespace DoomBubblesMod.Content.Items.HotS;

public class VerdantSpheres : ModItemWithTalents<FelInfusion, ManaTap, TwinSpheres>
{
    protected override Color? TalentColor => Color.Lime;

    public override int SoldBy => NPCID.Wizard;
    
    public override void SetStaticDefaults()
    {
        /* Tooltip.SetDefault("Flamestrike has increased radius and damage\n" +
                           "Living Bomb has extra pierce and costs no mana"); */
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 0;
        Item.height = 0;
        Item.accessory = true;
        Item.rare = ItemRarityID.Lime;
        Item.value = Item.buyPrice(0, 69);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<HotsPlayer>().verdant++;
        if (ChosenTalent is 1 or -1)
        {
            player.GetDamage(DamageClass.Generic) -= .1f;
            player.GetDamage(DamageClass.Magic) += .25f;
        }

        if (ChosenTalent is 2 or -1)
        {
            player.GetModPlayer<HotsPlayer>().manaTap = true;
        }

        if (ChosenTalent is 3 or -1)
        {
            player.GetModPlayer<HotsPlayer>().verdant++;
        }

        player.GetModPlayer<DoomBubblesPlayer>().NoManaItems.Add(ItemType<LivingBombWand>());
    }
}