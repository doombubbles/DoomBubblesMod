using System;
using System.Collections.Generic;
using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class SeekersArmguard : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seeker's Armguard");
            Tooltip.SetDefault("5% increased magic damage\n" +
                               "Gain up to 5 additional of each by killing enemies");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 12);
            item.width = 28;
            item.height = 28;
            item.rare = 4;
            item.accessory = true;
            item.defense = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += Math.Min(player.GetModPlayer<LoLPlayer>().seekerStacks / 4, 5);
            player.magicDamage += .05f + Math.Min(player.GetModPlayer<LoLPlayer>().seekerStacks / 400f, .05f);
        }

        public override void OnCraft(Recipe recipe)
        {
            Main.LocalPlayer.GetModPlayer<LoLPlayer>().seekerStacks = 0;
            base.OnCraft(recipe);
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            tooltips.Add(new TooltipLine(mod, "Seekers", "Current: +" + Math.Min(Main.LocalPlayer.GetModPlayer<LoLPlayer>().seekerStacks / 4.0, 5)));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ClothArmor"));
            recipe.AddIngredient(mod.ItemType("AmplifyingTome"));
            recipe.AddIngredient(mod.ItemType("ClothArmor"));
            recipe.AddIngredient(ItemID.SilverCoin, 65);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}