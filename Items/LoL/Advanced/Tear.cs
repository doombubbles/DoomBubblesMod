using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class Tear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tear of the Goddess");
            Tooltip.SetDefault("Increases maximum mana by 25\n" +
                               "10% reduced mana usage\n" +
                               "Gain up to 75 bonus mana by spending");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 8, 50);
            item.width = 34;
            item.height = 34;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 25 + player.GetModPlayer<LoLPlayer>().tearStacks;
        }

        public override void OnCraft(Recipe recipe)
        {
            Main.LocalPlayer.GetModPlayer<LoLPlayer>().tearStacks = 0;
            base.OnCraft(recipe);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SapphireCrystal"));
            recipe.AddIngredient(mod.ItemType("FaerieCharn"));
            recipe.AddIngredient(ItemID.GoldCoin, 3);
            recipe.AddIngredient(ItemID.SilverCoin, 75);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}