using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public abstract class ThoriumRecipeItem : ModItem
    {
        public override void AddRecipes()
        {
            if (DoomBubblesMod.thoriumLoaded.HasValue && DoomBubblesMod.thoriumLoaded.Value)
            {
                AddThoriumRecipe(ModLoader.GetMod("ThoriumMod"));
            }
        }

        public abstract void AddThoriumRecipe(Mod thoriumMod);
    }
}