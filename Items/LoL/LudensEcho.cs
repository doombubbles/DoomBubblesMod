using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace DoomBubblesMod.Items.LoL
{
    public class LudensEcho : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 90;
            item.magic = true;
            item.mana = 16;
            item.width = 48;    
            item.height = 50;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 32);
            item.rare = 8;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("Echo");
            Item.staff[item.type] = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luden's Echo");
            Tooltip.SetDefault("Casts an echoing bolt of unstable magic\n" +
                               "Equipped - 60 maximum mana, 10% cooldown reduction and magic damage\n" +
                               "Energized - Echo");
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            player.GetModPlayer<LoLPlayer>().ludens = true;
            player.statManaMax2 += 60;
            player.magicDamage += .1f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            player.GetModPlayer<LoLPlayer>().energized += 6;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LostChapter"));
            recipe.AddIngredient(mod.ItemType("BlastingWand"));
            recipe.AddIngredient(ItemID.GoldCoin, 10);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}