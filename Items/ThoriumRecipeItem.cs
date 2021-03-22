using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public abstract class ThoriumRecipeItem : ModItem
    {
        public override void AddRecipes()
        {
            if (DoomBubblesMod.thoriumMod != null)
            {
                AddThoriumRecipe(ModLoader.GetMod("ThoriumMod"));
            }
        }

        public abstract void AddThoriumRecipe(Mod thoriumMod);
    }
}