using System;
using System.Collections.Generic;
using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class JaurimsFist : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jaurim's Fist");
            Tooltip.SetDefault("5% increased melee damage" +
                               "Increases maximum life by 20\n" +
                               "Gain up to 20 additional by killing enemies");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 12);
            item.width = 34;
            item.height = 34;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20 + Math.Min(player.GetModPlayer<LoLPlayer>().jaurimStacks, 20);
        }

        public override void OnCraft(Recipe recipe)
        {
            Main.LocalPlayer.GetModPlayer<LoLPlayer>().jaurimStacks = 0;
            base.OnCraft(recipe);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            tooltips.Add(new TooltipLine(mod, "Jaurim", "Current: +" + Math.Min(Main.LocalPlayer.GetModPlayer<LoLPlayer>().jaurimStacks, 20)));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LongSword"));
            recipe.AddIngredient(mod.ItemType("RubyCrystal"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}