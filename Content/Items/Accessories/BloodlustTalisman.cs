namespace DoomBubblesMod.Content.Items.Accessories;

public class BloodlustTalisman : ModItem
{
    public override void SetStaticDefaults()
    {
        // Tooltip.SetDefault("Your lifesteal is uncapped");
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 32;
        Item.rare = ItemRarityID.Red;
        Item.accessory = true;
        Item.value = Item.buyPrice(2);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        //player.GetModPlayer<DoomBubblesPlayer>().bloodlust = true;
    }
}