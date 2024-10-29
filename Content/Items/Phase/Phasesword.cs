namespace DoomBubblesMod.Content.Items.Phase;

public abstract class Phasesword : ModItem
{
    public abstract int PhaseSaberId { get; }

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(PhaseSaberId);
        Item.damage = 69;
        Item.scale = 1.3f;
        Item.useTime = (int) (Item.useTime * .8f);
        Item.useAnimation = (int) (Item.useAnimation * .8f);
        Item.rare = ItemRarityID.Yellow;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(PhaseSaberId);
        recipe.AddIngredient(ItemID.Ectoplasm, 25);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}

public class RedPhasesword : Phasesword
{
    public override int PhaseSaberId => ItemID.RedPhasesaber;
}

public class BluePhasesword : Phasesword
{
    public override int PhaseSaberId => ItemID.BluePhasesaber;
}

public class GreenPhasesword : Phasesword
{
    public override int PhaseSaberId => ItemID.GreenPhasesaber;
}

public class PurplePhasesword : Phasesword
{
    public override int PhaseSaberId => ItemID.PurplePhasesaber;
}

public class YellowPhasesword : Phasesword
{
    public override int PhaseSaberId => ItemID.YellowPhasesaber;
}

public class OrangePhasesword : Phasesword
{
    public override int PhaseSaberId => ItemID.OrangePhasesaber;
}

public class WhitePhasesword : Phasesword
{
    public override int PhaseSaberId => ItemID.WhitePhasesaber;
}