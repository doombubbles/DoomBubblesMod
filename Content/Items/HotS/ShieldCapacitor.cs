using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Talent;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.HotS;

public class ShieldCapacitor : ModItemWithTalents<UnconqueredSpirit, DampeningField, PhotonicWeaponry>
{
    protected override Color? TalentColor => Color.Orange;

    public override int SoldBy => NPCID.Cyborg;

    public override void SetStaticDefaults()
    {
        // Tooltip.SetDefault("You quickly and constantly generate a 50 life shield.");
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 0;
        Item.height = 0;
        Item.accessory = true;
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.buyPrice(0, 69);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<HotsPlayer>().shieldCapacitorChosenTalent = ChosenTalent;
        player.SetThoriumProperty<int>("metalShieldMax", i => i + 50);
        player.GetModPlayer<HotsPlayer>().newShieldCapacitor = true;
    }
}