using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories;

public class TriggerFinger : ModItem
{
    public override void SetStaticDefaults()
    {
        /* Tooltip.SetDefault("Enables auto shoot for ranged weapons\n" +
                           "\"Kid named finger: reeeee\""); */
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 8;
        Item.height = 8;
        Item.value = Item.sellPrice(0, 2);
        Item.accessory = true;
        Item.rare = ItemRarityID.Orange;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDoomBubblesPlayer().autoShoot = true;
    }
}