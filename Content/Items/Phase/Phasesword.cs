using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Phase;

public abstract class Phasesword : ModItem
{
    public abstract int PhaseSaberID { get; }

    public override void SetStaticDefaults()
    {
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(PhaseSaberID);
        Item.damage = 69;
        Item.scale = 1.3f;
        Item.useTime = (int) (Item.useTime * .8f);
        Item.useAnimation = (int) (Item.useAnimation * .8f);
        Item.rare = ItemRarityID.Yellow;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(PhaseSaberID);
        recipe.AddIngredient(ItemID.Ectoplasm, 25);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}

public class RedPhasesword : Phasesword
{
    public override int PhaseSaberID => ItemID.RedPhasesaber;
}

public class BluePhasesword : Phasesword
{
    public override int PhaseSaberID => ItemID.BluePhasesaber;
}

public class GreenPhasesword : Phasesword
{
    public override int PhaseSaberID => ItemID.GreenPhasesaber;
}

public class PurplePhasesword : Phasesword
{
    public override int PhaseSaberID => ItemID.PurplePhasesaber;
}

public class YellowPhasesword : Phasesword
{
    public override int PhaseSaberID => ItemID.YellowPhasesaber;
}

public class OrangePhasesword : Phasesword
{
    public override int PhaseSaberID => ItemID.OrangePhasesaber;
}

public class WhitePhasesword : Phasesword
{
    public override int PhaseSaberID => ItemID.WhitePhasesaber;
}