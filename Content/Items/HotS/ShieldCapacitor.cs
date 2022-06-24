using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Talent;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.HotS;

public class ShieldCapacitor : ModItemWithTalents<TalentUnconqueredSpirit, TalentDampeningField, TalentPhotonicWeaponry>
{
    protected override Color? TalentColor => Color.Orange;


    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Shield Capacitor");
        Tooltip.SetDefault("You quickly and constantly generate a 50 life shield.");
        SacrificeTotal = 1;
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
        player.GetModPlayer<HotSPlayer>().shieldCapacitorChosenTalent = ChosenTalent;
        player.SetThoriumProperty<int>("metalShieldMax", i => i + 50);
        player.GetModPlayer<HotSPlayer>().newShieldCapactior = true;
    }
}