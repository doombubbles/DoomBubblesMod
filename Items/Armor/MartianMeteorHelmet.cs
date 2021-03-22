using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MartianMeteorHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Martian Meteor Helmet");
            Tooltip.SetDefault("17% Increased Magic Damage");
        }

        public override void SetDefaults()
        {
            var realSlot = item.headSlot;
            item.CloneDefaults(ItemID.MeteorHelmet);
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(0, 22, 50);
            item.defense = 15;
            item.headSlot = realSlot;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MartianMeteorSuit") && legs.type == mod.ItemType("MartianMeteorLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Laser Machinegun costs 0 mana";
            player.GetModPlayer<DoomBubblesPlayer>().noManaItems.Add(ItemID.LaserMachinegun);
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += .17f;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LaserMeteorHelmet"));
            recipe.AddIngredient(ItemID.MartianConduitPlating, 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}