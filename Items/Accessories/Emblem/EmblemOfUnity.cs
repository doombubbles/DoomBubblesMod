using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Emblem
{
    public class EmblemOfUnity : ThoriumRecipeItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased damage\n" +
                               "+10% damage for each other player wearing this");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.accessory = true;
            item.rare = 6;
            item.value = Item.sellPrice(0, 8, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += .1f;
            player.GetModPlayer<DoomBubblesPlayer>().united = true;
        }

        public override void AddThoriumRecipe(Mod thoriumMod)
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(thoriumMod.ItemType("RingofUnity"));
            recipe.AddIngredient(ItemID.AvengerEmblem);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}