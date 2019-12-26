using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public class BrokenHeroGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Hero Gun");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 26;
            item.maxStack = 99;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 8;
        }

        public override void AddRecipes()
        {
            if (DoomBubblesMod.thoriumLoaded.HasValue && DoomBubblesMod.thoriumLoaded.Value)
            {
                ThoriumRecipes();
            }
        }

        public void ThoriumRecipes()
        {
            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(this);
            recipe1.SetResult(ModLoader.GetMod("ThoriumMod").ItemType("BrokenHeroFragment"), 2);
            recipe1.AddTile(TileID.MythrilAnvil);
            recipe1.AddRecipe();
            
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.SetResult(this);
            recipe2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("BrokenHeroFragment"), 2);
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.AddRecipe();
            
            ModRecipe recipe3 = new ModRecipe(mod);
            recipe3.AddIngredient(ItemID.BrokenHeroSword);
            recipe3.SetResult(ModLoader.GetMod("ThoriumMod").ItemType("BrokenHeroFragment"), 2);
            recipe3.AddTile(TileID.MythrilAnvil);
            recipe3.AddRecipe();
            
            ModRecipe recipe4 = new ModRecipe(mod);
            recipe4.SetResult(ItemID.BrokenHeroSword);
            recipe4.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("BrokenHeroFragment"), 2);
            recipe4.AddTile(TileID.MythrilAnvil);
            recipe4.AddRecipe();
        }
    }
}