namespace DoomBubblesMod.Content.Items.Misc;

public class SprayPaint : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Applies dye to held items");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.CritterShampoo);
    }
}