using DoomBubblesMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    internal class ExplosiveCharm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosive Charm");
            Tooltip.SetDefault("Explosive Bullets deal more damage and can no longer hurt you");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 2);
            Item.width = 28;
            Item.height = 26;
            Item.rare = 4;
            Item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>().explosionBulletBonus = true;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ExplosivePowder, 100);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}