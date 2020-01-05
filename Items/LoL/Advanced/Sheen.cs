using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class Sheen : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("After hitting with a projectile, your next swing deals bonus melee damage\n" +
                               "Equipped - 25 mana and 10% cdr");
        }

        public override void SetDefaults()
        {
            item.damage = 35;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 10, 50);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.scale = 1.2f;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().sheen = true;
            player.GetModPlayer<LoLPlayer>().sheen2 = true;
            player.statManaMax2 += 25;
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            player.GetModPlayer<LoLPlayer>().sheen2 = true;
            base.HoldItem(player);
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            player.GetModPlayer<LoLPlayer>().sheen = true;
            base.UpdateInventory(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SapphireCrystal"));
            recipe.AddIngredient(ItemID.GoldCoin, 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}