using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class AegisOfTheLegion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aegis of the Legion");
            Tooltip.SetDefault("Reduces damage taken by 3%\n" +
                               "All players on your team gain also get these bonuses");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 11);
            item.width = 24;
            item.height = 28;
            item.rare = 4;
            item.accessory = true;
            item.defense = 3;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += .03f;

            for (var i = 0; i < Main.player.Length; i++)
            {
                var playir = Main.player[i];
                if (playir != null && playir.active && !player.dead && player.team == playir.team)
                {
                    playir.AddBuff(mod.BuffType("Aegis"), 2);
                }
            }
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("NullMagicMantle"));
            recipe.AddIngredient(mod.ItemType("ClothArmor"));
            recipe.AddIngredient(ItemID.GoldCoin, 3);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}