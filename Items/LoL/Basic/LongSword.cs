using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Basic
{
    public class LongSword : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 42;
            item.height = 42;

            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 3, 50);
            item.rare = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
        }
    }
}