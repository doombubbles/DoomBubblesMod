using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    class TargetingComputer : ModItem
    {
        public override void SetDefaults()
        {

            item.value = 42069;
            item.width = 30;
            item.height = 20;
            item.rare = -12;
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Targeting Computer");
            Tooltip.SetDefault("Who needs the Force anyway?");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>(mod).homing = true;
        }
    }
}
