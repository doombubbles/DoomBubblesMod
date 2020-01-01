using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class Mejais : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases maximum mana by 20\n" +
                               "25% increased healing from potions\n" +
                               "Increases magic damage by up to 20% from killing enemies");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 14);
            item.width = 26;
            item.height = 30;
            item.rare = 4;
            item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            if (item.owner != 255 && item.owner != -1)
                tooltips.Add(new TooltipLine(mod, "Dread",
                    "Current: " + Math.Min(Main.LocalPlayer.GetModPlayer<LoLPlayer>().dread / 2, 20) + "%"));
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 20;
            player.GetModPlayer<DoomBubblesPlayer>().potionHealing += .25f;
            player.magicDamage += Math.Min(player.GetModPlayer<LoLPlayer>().dread / 100f, .2f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DarkSeal"));
            recipe.AddIngredient(ItemID.GoldCoin, 10);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
    }
}