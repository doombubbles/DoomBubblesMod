using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Basic
{
    public class BlastingWand : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;
            item.mana = 20;
            item.width = 32;
            item.height = 32;

            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 8, 50);
            item.rare = 1;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shootSpeed = 7f;
            item.shoot = ProjectileID.InfernoFriendlyBlast;
            Item.staff[item.type] = true;
        }
    }
}