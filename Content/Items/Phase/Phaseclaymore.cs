namespace DoomBubblesMod.Content.Items.Phase;

public abstract class Phaseclaymore<T> : ModItem where T : Phasesword
{
    public override void SetStaticDefaults()
    {
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(GetInstance<T>().PhaseSaberId);
        Item.damage = 169;
        Item.scale = 1.45f;
        Item.useTime = (int) (Item.useTime * .8f * .8f);
        Item.useAnimation = (int) (Item.useAnimation * .8f * .8f);
        Item.rare = ItemRarityID.Red;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemType<T>());
        recipe.AddIngredient(ItemID.FragmentNebula, 5);
        recipe.AddIngredient(ItemID.FragmentSolar, 5);
        recipe.AddIngredient(ItemID.FragmentStardust, 5);
        recipe.AddIngredient(ItemID.FragmentVortex, 5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}

public class RedPhaseclaymore : Phaseclaymore<RedPhasesword>
{
}

public class BluePhaseclaymore : Phaseclaymore<BluePhasesword>
{
}

public class GreenPhaseclaymore : Phaseclaymore<GreenPhasesword>
{
}

public class PurplePhaseclaymore : Phaseclaymore<PurplePhasesword>
{
}

public class YellowPhaseclaymore : Phaseclaymore<YellowPhasesword>
{
}

public class OrangePhaseclaymore : Phaseclaymore<OrangePhasesword>
{
}

public class WhitePhaseclaymore : Phaseclaymore<WhitePhasesword>
{
}

/*public class BlackPhaseclaymore : Phaseclaymore<BlackPhasesword>
{
}*/

public class CyanPhaseclaymore : Phaseclaymore<CyanPhasesword>
{
}

public class PinkPhaseclaymore : Phaseclaymore<PinkPhasesword>
{
}