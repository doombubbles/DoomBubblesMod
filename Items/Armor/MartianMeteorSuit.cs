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
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            var realSlot = Item.bodySlot;
            Item.CloneDefaults(ItemID.MeteorSuit);
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 15);
            Item.defense = 18;
            Item.bodySlot = realSlot;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<MartianMeteorHelmet>() &&
                   legs.type == ModContent.ItemType<MartianMeteorLeggings>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += .17f;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LaserMeteorSuit>());
            recipe.AddIngredient(ItemID.MartianConduitPlating, 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}