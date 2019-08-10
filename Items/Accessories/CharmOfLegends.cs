using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    class CharmOfLegends : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charm of Legends");
            Tooltip.SetDefault("Reduces the cooldown of healing potions\n"
                               + "Health/mana always regenerates as if you weren't moving\n"
                               + "Incrases maximum mana by 20\n"
                               + "Increases mana and life regeneration rate");
        }

        public override void SetDefaults()
        {
            var real = item.handOnSlot;
            item.CloneDefaults(ItemID.CharmofMyths);
            item.handOnSlot = real;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>().sStone = true;
            player.statManaMax2 += 20;
            player.manaRegenDelayBonus++;
            player.manaRegenBonus += 25;
            player.lifeRegen++;
            player.pStone = true;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CharmofMyths);
            recipe.AddIngredient(mod.ItemType("CharmOfFables"));
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
