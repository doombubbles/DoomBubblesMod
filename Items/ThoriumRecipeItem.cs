using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public abstract class ThoriumRecipeItem : ModItem
    {
        public override void AddRecipes()
        {
            if (DoomBubblesMod.ThoriumMod != null)
            {
                AddThoriumRecipe(DoomBubblesMod.ThoriumMod);
            }
        }

        public abstract void AddThoriumRecipe(Mod thoriumMod);
    }
}