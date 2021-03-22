using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class LaserMeteorLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Laser Meteor Leggings");
            Tooltip.SetDefault("11% Increased Magic Damage");
        }

        public override void SetDefaults()
        {
            var realSlot = item.legSlot;
            item.CloneDefaults(ItemID.MeteorLeggings);
            item.rare = ItemRarityID.LightRed;
            item.value = Item.sellPrice(0, 4, 50);
            item.defense = 10;
            item.legSlot = realSlot;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == mod.ItemType("LaserMeteorHelmet") && body.type == mod.ItemType("LaserMeteorSuit");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += .11f;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteorLeggings);
            recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}