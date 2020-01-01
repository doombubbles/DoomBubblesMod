using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class FiendishCodex : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Equipped - 10% increased magic damage and cooldown reduction");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.damage = 45;
            item.useTime = 20;
            item.useAnimation = 20;
            item.height = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.mana = 10;
            item.value = Item.buyPrice(0, 9);
            item.rare = 4;
            item.shoot = ProjectileID.BlackBolt;
            item.shootSpeed = 10f;
        }
        
        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void HoldItem(Player player)
        {
            player.magicDamage += .1f;
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            base.HoldItem(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.magicDamage += .1f;
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.ranged = false;
            proj.magic = true;
            return false;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AmplifyingTome"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddIngredient(ItemID.SilverCoin, 65);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}