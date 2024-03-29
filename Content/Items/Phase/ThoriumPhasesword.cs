namespace DoomBubblesMod.Content.Items.Phase;

public abstract class ThoriumPhasesword : Phasesword
{
    public override int PhaseSaberId => ItemID.RedPhasesaber;

    protected abstract string PhasesaberModItem { get; }

    public override void AddRecipes()
    {
        if (DoomBubblesMod.ThoriumMod != null)
        {
            AddThoriumRecipe();
        }
    }

    private void AddThoriumRecipe()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(DoomBubblesMod.ThoriumMod.Find<ModItem>(PhasesaberModItem));
        recipe.AddIngredient(ItemID.Ectoplasm, 25);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}

public class BlackPhasesword : ThoriumPhasesword
{
    protected override string PhasesaberModItem => "BlackPhasesaber";
}

public class CyanPhasesword : ThoriumPhasesword
{
    protected override string PhasesaberModItem => "CyanPhasesaber";
}

public class PinkPhasesword : ThoriumPhasesword
{
    protected override string PhasesaberModItem => "PinkPhasesaber";
}