using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Accessories.Emblem
{
    public class ChromaticEmblem : ThoriumRecipeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chromatic Emblem");
            Tooltip.SetDefault("15% increased damage\n" +
                               "15% increased crit chance\n" +
                               "15% increased attack speed\n" +
                               "15% reduced damage taken\n" +
                               "Increases armor penetration by 15\n" +
                               "Gravity Globe benefits ;)\n" +
                               "Can't be equipped with any other 'Emblems'");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(1);
            item.width = 28;
            item.height = 28;
            item.rare = -12;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += .15f;
            player.AllCrit(i => i + 15);
            player.endurance += .15f;
            player.armorPenetration += 15;
            player.AttackSpeed(f => f + .15f);

            player.gravControl2 = true;
            player.gravControl = true;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (player.GetModPlayer<DoomBubblesPlayer>().emblem != 0)
            {
                return false;
            }

            return base.CanEquipAccessory(player, slot);
        }

        public override void AddThoriumRecipe(Mod thoriumMod)
        {
            var recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddIngredient(mod.ItemType("TimsRegret"));
            recipe.AddIngredient(mod.ItemType("ChromaticGauntlet"));
            recipe.AddIngredient(thoriumMod.ItemType("TheRing"));
            recipe.AddIngredient(mod.ItemType("NebulaEmblem"));
            recipe.AddIngredient(mod.ItemType("SolarEmblem"));
            recipe.AddIngredient(mod.ItemType("StardustEmblem"));
            recipe.AddIngredient(mod.ItemType("VortexEmblem"));
            recipe.AddIngredient(mod.ItemType("ShootingStarEmblem"));
            recipe.AddIngredient(mod.ItemType("HeavenlyEmblem"));
            recipe.AddIngredient(mod.ItemType("WhiteDwarfEmblem"));
            recipe.AddIngredient(ItemID.GravityGlobe);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}