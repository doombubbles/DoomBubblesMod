namespace DoomBubblesMod.Content.Items.Talent;

public class TalentTwinSpheres : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Talent: Twin Spheres");
        Tooltip.SetDefault("Verdant Spheres Talent\n" +
                           "Base Verdant Sphere bonuses are doubled\n" +
                           "[Right Click on a Verdant Spheres with this to apply]");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.maxStack = 1;
        Item.value = Item.buyPrice(0, 42);
        Item.rare = ItemRarityID.Lime;
    }
}