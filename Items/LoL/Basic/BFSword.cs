using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Basic
{
    public class BFSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("B. F. Sword");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.melee = true;
            item.width = 54;
            item.height = 54;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = Item.buyPrice(0, 13);
            item.rare = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
        }
    }
}