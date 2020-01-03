using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class CatalystOfAeons : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases maximum life by 20\n" +
                               "Increases maximum mana by 30\n" +
                               "Restores mana when taking damage\n" +
                               "Restores health when expending mana");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 11);
            item.width = 24;
            item.height = 24;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.statManaMax2 += 30;
            player.magicCuffs = true;
            player.GetModPlayer<LoLPlayer>().eternity = true;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RubyCrystal"));
            recipe.AddIngredient(mod.ItemType("SapphireCrystal"));
            recipe.AddIngredient(ItemID.GoldCoin, 3);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}