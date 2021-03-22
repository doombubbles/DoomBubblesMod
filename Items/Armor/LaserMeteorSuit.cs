using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class LaserMeteorSuit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Laser Meteor Suit");
            Tooltip.SetDefault("11% Increased Magic Damage");
        }

        public override void SetDefaults()
        {
            var realSlot = item.bodySlot;
            item.CloneDefaults(ItemID.MeteorSuit);
            item.rare = ItemRarityID.LightRed;
            item.value = Item.sellPrice(0, 4, 50);
            item.defense = 12;
            item.bodySlot = realSlot;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == mod.ItemType("LaserMeteorHelmet") && legs.type == mod.ItemType("LaserMeteorLeggings");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += .11f;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteorSuit);
            recipe.AddIngredient(ItemID.CrystalShard, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}