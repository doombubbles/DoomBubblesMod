using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL
{
    public class RodOfAges : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rod of Ages");
            Tooltip.SetDefault("Increases maximum life and mana by 30, and magic damage by 10%\n" +
                               "Restores mana when taking damage and health when spending mana\n" +
                               "Gains up to 20 more life/mana and 5% more damage after 10 minutes");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.magic = true;
            item.damage = 60;
            item.mana = 6;
            item.useTime = 10;
            item.useAnimation = 10;
            item.knockBack = 5;
            item.useStyle = 5;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.value = Item.buyPrice(0, 26, 50);
            item.width = 38;
            item.height = 38;
            item.rare = 8;
            item.shoot = mod.ProjectileType("Roa");
            item.shootSpeed = 10f;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if ((int) Main.time % 3600 == 0) player.GetModPlayer<LoLPlayer>().roa = Math.Min(player.GetModPlayer<LoLPlayer>().roa + 1, 10);
            
            player.statLifeMax2 += 30 + 2 * player.GetModPlayer<LoLPlayer>().roa;
            player.statManaMax2 += 30 + 2 * player.GetModPlayer<LoLPlayer>().roa;;
            player.magicDamage += .1f + .005f * player.GetModPlayer<LoLPlayer>().roa;
            player.magicCuffs = true;
            player.GetModPlayer<LoLPlayer>().eternity = true;
        }
        
        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            tooltips.Add(new TooltipLine(mod, "RoA", "Stacks: " + Main.LocalPlayer.GetModPlayer<LoLPlayer>().roa));
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CatalystOfAeons"));
            recipe.AddIngredient(mod.ItemType("BlastingWand"));
            recipe.AddIngredient(ItemID.GoldCoin, 6);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}