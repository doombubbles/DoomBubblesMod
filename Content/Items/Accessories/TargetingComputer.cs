using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Items.Accessories;

public class TargetingComputer : ModItem
{
    public override void SetDefaults()
    {
        Item.value = 42069;
        Item.width = 30;
        Item.height = 20;
        Item.rare = -12;
        Item.accessory = true;
    }

    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Who needs the Force anyway?");
        SacrificeTotal = 1;
    }


    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<DoomBubblesPlayer>().homing = true;
    }
}