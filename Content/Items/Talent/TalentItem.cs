using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Talent;

public abstract class TalentItem : ModItem
{
    public abstract string Description { get; }
    protected abstract int RarityColor { get; }

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.maxStack = 1;
        Item.value = Item.buyPrice(0, 42);
        Item.rare = RarityColor;
    }
}

public abstract class TalentItem<T> : TalentItem where T : ModItemWithTalents
{
    public override void SetStaticDefaults()
    {
        GetInstance<T>().AutoStaticDefaults();
        GetInstance<T>().SetStaticDefaults();
        // DisplayName.SetDefault("Talent: " + Name.ToNameFormat());
        // Tooltip.SetDefault($"{GetInstance<T>().DisplayName.GetDefault()} Talent\n" + Description);
    }
}