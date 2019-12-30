using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class KircheisShard : ModItem
    {
        public override void SetStaticDefaults()
        {   
            Tooltip.SetDefault("Energized - Jolt\nEnergized attacks deal 80 bonus damage");
        }
        
        public override void SetDefaults()
        {
            item.damage = 30;
            item.melee = true;
            item.width = 32;
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
            player.GetModPlayer<LoLPlayer>().jolt = true;
            base.HoldItem(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<LoLPlayer>().jolt = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.GetModPlayer<LoLPlayer>().energized += 6;
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Dagger"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}