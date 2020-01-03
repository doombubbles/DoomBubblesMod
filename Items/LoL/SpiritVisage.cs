using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL
{
    public class SpiritVisage : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases maximum life by 45\n" +
                               "10% increased cooldown reduction\n" +
                               "10% reduced damage taken\n" +
                               "All self healing is increased");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 28);
            item.width = 24;
            item.height = 24;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 45;
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            player.endurance += .1f;
            player.GetModPlayer<LoLPlayer>().healingBonus += .3f;
            player.lifeRegen += 3;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RubyCrystal"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}