using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class GlacialShroud : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased cooldown reduction\n" +
                               "Increases maximum mana by 25");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 9);
            item.width = 32;
            item.height = 30;
            item.rare = 4;
            item.accessory = true;
            item.defense = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            player.statManaMax2 += 25;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SapphireCrystal"));
            recipe.AddIngredient(mod.ItemType("ClothArmor"));
            recipe.AddIngredient(ItemID.GoldCoin, 2);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}