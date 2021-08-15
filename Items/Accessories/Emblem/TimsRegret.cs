using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Emblem
{
    internal class TimsRegret : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tim's Regret");
            Tooltip.SetDefault("15% increased damage\n100% increased n00b regret");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.value = 100000;
            Item.width = 28;
            Item.height = 28;
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += .15f;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WarriorEmblem);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddIngredient(ItemID.RangerEmblem);
            recipe.AddIngredient(ItemID.SummonerEmblem);


            if (DoomBubblesMod.thoriumMod != null)
            {
                AddThoriumRecipes(ref recipe);
            }

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        private void AddThoriumRecipes(ref Recipe recipe)
        {
            var thoriumMod = DoomBubblesMod.thoriumMod;
            recipe.AddIngredient(thoriumMod.Find<ModItem>("BardEmblem"));
            recipe.AddIngredient(thoriumMod.Find<ModItem>("NinjaEmblem"));
            recipe.AddIngredient(thoriumMod.Find<ModItem>("ClericEmblem"));
        }
    }
}