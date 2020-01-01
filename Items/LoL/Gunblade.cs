using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class Gunblade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hextech Gunblade");
            Tooltip.SetDefault("15% omnivamp\n" +
                               "Also scales with melee and magic bonuses");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.ranged = true;
            item.width = 54;
            item.height = 54;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 34);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.useAmmo = AmmoID.Bullet;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 10f;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().omnivamp += .15f;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().omnivamp += .15f;
            base.HoldItem(player);
        }
        
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += player.magicDamage + player.meleeDamage - 2;
            mult *= player.magicDamageMult * player.meleeDamageMult;
            base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += player.magicCrit + player.meleeCrit;
            base.GetWeaponCrit(player, ref crit);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BilgewaterCutlass"));
            recipe.AddIngredient(mod.ItemType("HextechRevolver"));
            recipe.AddIngredient(ItemID.GoldCoin, 7);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        

    }
}
