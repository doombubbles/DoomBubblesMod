using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    internal class CharmOfEpics : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charm of Epics");
            Tooltip.SetDefault("Reduces the cooldown of healing potions\n" +
                               "Incrases maximum mana by 20\n" +
                               "Improves life/mana regen in many ways\n" +
                               "Increases heart/star pickup range\n" +
                               "Regenerate mana on taking damage; health on dealing");
        }

        public override void SetDefaults()
        {
            var real = item.handOnSlot;
            item.CloneDefaults(ItemID.CharmofMyths);
            item.handOnSlot = real;
            item.value = Item.sellPrice(1);
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DoomBubblesPlayer>().sStone = true;
            player.statManaMax2 += 20;
            player.manaRegenDelayBonus++;
            player.manaRegenBonus += 25;
            player.lifeRegen++;
            player.pStone = true;
            player.crimsonRegen = true;
            player.shinyStone = true;
            player.lifeMagnet = true;
            player.manaMagnet = true;
            player.magicCuffs = true;
            player.onHitRegen = true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CharmOfLegends"));
            recipe.AddIngredient(ItemID.ShinyStone);
            recipe.AddIngredient(mod.ItemType("CrimsonVoodooDoll"));
            recipe.AddIngredient(mod.ItemType("PalladiumVoodooDoll"));
            recipe.AddIngredient(ItemID.CelestialCuffs);
            recipe.AddIngredient(mod.GetItem("CardiopulmonaryMagnet"));
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}