using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
namespace DoomBubblesMod.Items.LoL.Basic
{
    public class DarkSeal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases maximum mana by 10\n" +
                               "+10% increased healing from potions\n" +
                               "Increases magic damage by up to 10% from killing enemies");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 3, 50);
            item.width = 40;
            item.height = 44;
            item.rare = 1;
            item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            tooltips.Add(new TooltipLine(mod, "Dread", "Current: " + (Main.player[item.owner].GetModPlayer<LoLPlayer>().dread / 2) + "%"));
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 10;
            player.GetModPlayer<DoomBubblesPlayer>().potionHealing += .10f;
            player.magicDamage += Math.Min(player.GetModPlayer<LoLPlayer>().dread / 200f, .1f);
        }
        
    }
}