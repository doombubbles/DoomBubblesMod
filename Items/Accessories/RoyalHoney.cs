using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    internal class RoyalHoney : ModItem
    {
        public override void SetDefaults()
        {
            item.value = 42069;
            item.width = 20;
            item.height = 30;
            item.rare = -12;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Royal Honey");
            Tooltip.SetDefault("Bees are friendly");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.npcTypeNoAggro[NPCID.Bee] = true;
            player.npcTypeNoAggro[NPCID.BeeSmall] = true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledHoney);
            recipe.AddIngredient(ItemID.GoldCrown);
            recipe.AddIngredient(ItemID.RoyalGel);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();

            var recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.BottledHoney);
            recipe2.AddIngredient(ItemID.PlatinumCrown);
            recipe2.AddIngredient(ItemID.RoyalGel);
            recipe2.AddTile(TileID.DemonAltar);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}