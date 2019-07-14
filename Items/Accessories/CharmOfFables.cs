using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    class CharmOfFables : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charm of Fables");
            Tooltip.SetDefault("Health/mana always regenerates as if you weren't moving\n"
                               + "Incrases maximum mana by 20\n"
                               + "Increases mana regeneration rate");
        }

        public override void SetDefaults()
        {
            var real = item.handOnSlot;
            item.CloneDefaults(ItemID.CharmofMyths);
            item.handOnSlot = real;
            item.lifeRegen = 0;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>().sStone = true;
            player.statManaMax2 += 20;
            player.manaRegenDelayBonus++;
            player.manaRegenBonus += 25;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SorcerersStone"));
            recipe.AddIngredient(ItemID.ManaRegenerationBand);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
