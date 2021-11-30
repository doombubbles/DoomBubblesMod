using DoomBubblesMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Emblem
{
    internal class WhiteDwarfEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Dwarf Emblem");
            Tooltip.SetDefault("20% increased throwing damage");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 5);
            Item.width = 28;
            Item.height = 28;
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Throwing) += .2f;
        }

        public override void AddRecipes()
        {
            if (DoomBubblesMod.ThoriumMod != null)
            {
                var recipe = CreateRecipe();
                addThoriumRecipe(ref recipe);
                recipe.AddIngredient(ItemID.LunarBar, 5);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.ReplaceResult(this);
                recipe.Register();
            }
        }

        public void addThoriumRecipe(ref Recipe recipe)
        {
            var thoriumMod = DoomBubblesMod.ThoriumMod;
            recipe.AddIngredient(thoriumMod.Find<ModItem>("NinjaEmblem"));
        }
    }
}