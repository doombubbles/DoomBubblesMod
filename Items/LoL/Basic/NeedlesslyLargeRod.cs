using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Basic
{
    public class NeedlesslyLargeRod : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 30;
            item.magic = true;
            item.mana = 20;
            item.width = 32;
            item.height = 32;

            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 12, 50);
            item.rare = 1;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shootSpeed = 10f;
            item.shoot = ProjectileID.CrystalPulse;
            Item.staff[item.type] = true;
        }
    }
}