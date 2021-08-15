using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    public class BloodlustTalisman : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodlust Talisman");
            Tooltip.SetDefault("Your lifesteal is uncapped");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 32;
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
            Item.value = Item.buyPrice(2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //player.GetModPlayer<DoomBubblesPlayer>().bloodlust = true;
        }
    }
}