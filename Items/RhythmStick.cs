using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public class RhythmStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\"Hit me with your rhythm stick.\"\n\t-some song I listened to");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;
            item.maxStack = 1;
            item.value = 100;
            item.rare = 1;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/RhythmStick");
            item.useStyle = 1;
            item.useAnimation = 360;
            item.useTime = 360;
            // Set other item.X values here
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
        
    }
}