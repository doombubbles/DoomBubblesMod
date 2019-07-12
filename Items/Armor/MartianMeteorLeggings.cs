using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class MartianMeteorLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Martian Meteor Leggings");
            Tooltip.SetDefault("17% Increased Magic Damage");
        }

        public override void SetDefaults()
        {
            int realSlot = item.legSlot;
            item.CloneDefaults(ItemID.MeteorLeggings);
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(0, 15, 0, 0);
            item.defense = 15;
            item.legSlot = realSlot;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == mod.ItemType("MartianMeteorHelmet") && body.type == mod.ItemType("MartianMeteorSuit");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += .17f;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LaserMeteorLeggings"));
            recipe.AddIngredient(ItemID.MartianConduitPlating, 75);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}