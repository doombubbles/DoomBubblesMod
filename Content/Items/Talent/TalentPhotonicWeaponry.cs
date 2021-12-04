using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Talent;

public class TalentPhotonicWeaponry : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Photonic Weaponry");
        Tooltip.SetDefault("Shield Capacitor Talent\n" +
                           "You deal 15% more damage while Shield Capacitor is above half charge\n" +
                           "[Right Click on a Shield Capacitor with this to apply]");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.maxStack = 1;
        Item.value = Item.buyPrice(0, 42);
        Item.rare = ItemRarityID.Orange;
    }
}