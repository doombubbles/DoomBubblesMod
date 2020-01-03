using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class SerratedDirk : ModItem
    {
        public override void SetStaticDefaults()
        {   
            Tooltip.SetDefault("10 bonus armor penetration");
        }
        
        public override void SetDefaults()
        {
            item.damage = 30;
            item.melee = true;
            item.width = 36;
            item.height = 32;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 3;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 7);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void HoldItem(Player player)
        {
            player.armorPenetration += 10;
            base.HoldItem(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.armorPenetration += 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LongSword"));
            recipe.AddIngredient(mod.ItemType("LongSword"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}