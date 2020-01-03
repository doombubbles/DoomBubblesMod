using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class ChaliceOfHarmony : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased mana regen\n" +
                               "Reduces damage taken by 6%\n" +
                               "Gain mana regen from your increased health regen");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 8);
            item.width = 28;
            item.height = 42;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaRegenBonus += 20;
            player.endurance += .06f;
            player.GetModPlayer<LoLPlayer>().harmony = true;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("FaerieCharm"));
            recipe.AddIngredient(mod.ItemType("NullMagicMantle"));
            recipe.AddIngredient(mod.ItemType("FaerieCharm"));
            recipe.AddIngredient(ItemID.GoldCoin, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}