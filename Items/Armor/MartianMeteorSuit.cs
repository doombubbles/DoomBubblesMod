using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class MartianMeteorSuit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Martian Meteor Suit");
            Tooltip.SetDefault("17% Increased Magic Damage");
        }

        public override void SetDefaults()
        {
            var realSlot = item.bodySlot;
            item.CloneDefaults(ItemID.MeteorSuit);
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(0, 15);
            item.defense = 18;
            item.bodySlot = realSlot;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == mod.ItemType("MartianMeteorHelmet") &&
                   legs.type == mod.ItemType("MartianMeteorLeggings");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += .17f;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LaserMeteorSuit"));
            recipe.AddIngredient(ItemID.MartianConduitPlating, 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}