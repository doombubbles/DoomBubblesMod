using DoomBubblesMod.Content.Items.Misc;

namespace DoomBubblesMod.Content.Items.Phase;

public abstract class ThoriumPhasesword : Phasesword, IHasThoriumRecipe
{
    public override int PhaseSaberId => ItemID.RedPhasesaber;

    protected abstract string PhasesaberModItem { get; }

    public void AddThoriumRecipe(Mod thoriumMod)
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(thoriumMod.Find<ModItem>(PhasesaberModItem));
        recipe.AddIngredient(ItemID.Ectoplasm, 25);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}

/*public class BlackPhasesword : ThoriumPhasesword
{
    protected override string PhasesaberModItem => "BlackPhasesaber";
}*/

public class CyanPhasesword : ThoriumPhasesword
{
    protected override string PhasesaberModItem => "CyanPhasesaber";
}

public class PinkPhasesword : ThoriumPhasesword
{
    protected override string PhasesaberModItem => "PinkPhasesaber";
}