using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    [AutoloadEquip(EquipType.Back)]
    class NegatronCloak : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Negatron Cloak");
            Tooltip.SetDefault("Reduces damage taken by 4%");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 7, 20);
            item.width = 24;
            item.height = 24;
            item.rare = 4;
            item.accessory = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += .04f;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("NullMagicMantle"));
            recipe.AddIngredient(ItemID.GoldCoin, 2);
            recipe.AddIngredient(ItemID.SilverCoin, 70);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
