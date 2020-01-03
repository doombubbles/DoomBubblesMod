using System.Collections.Generic;
using DoomBubblesMod.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoomBubblesMod.Items.LoL
{
    public class RunePage : ModItem
    {
        public RunePath primaryPath;
        public int keystone;
        
        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 5);
            item.width = 30;
            item.height = 30;
            item.rare = 1;
            item.accessory = true;
        }

        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            tag.Set("primaryPath", (int) primaryPath);
            tag.Set("keystone", keystone);
            return tag;
        }
        
        public override void Load(TagCompound tag)
        {
            primaryPath = (RunePath) tag.GetInt("primaryPath");
            keystone = tag.GetInt("keystone");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "RunePath", "Primary: " + primaryPath));
            tooltips.Add(new TooltipLine(mod, "Keystone", "Keystone: " + (keystone == 0 ? "None" : RunesUI.KeystoneName(keystone, primaryPath))));
            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().primaryPath = primaryPath;
            player.GetModPlayer<LoLPlayer>().keystone = keystone;
        }

        public override ModItem Clone()
        {
            RunePage newItem = (RunePage) base.Clone();
            newItem.primaryPath = primaryPath;
            newItem.keystone = keystone;
            return newItem;
        }
        
        public override bool CloneNewInstances => true;
    }
}