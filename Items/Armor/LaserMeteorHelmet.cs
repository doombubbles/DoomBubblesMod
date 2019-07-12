using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class LaserMeteorHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Laser Meteor Helmet");
            Tooltip.SetDefault("11% Increased Magic Damage");
        }

        public override void SetDefaults()
        {
            int realSlot = item.headSlot;
            item.CloneDefaults(ItemID.MeteorHelmet);
            item.rare = ItemRarityID.LightRed;
            item.value = Item.sellPrice(0, 6, 75, 0);
            item.defense = 10;
            item.headSlot = realSlot;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("LaserMeteorSuit") && legs.type == mod.ItemType("LaserMeteorLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Laser Rifle costs 0 mana";
            player.GetModPlayer<DoomBubblesPlayer>().noManaItems.Add(ItemID.LaserRifle);
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += .11f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteorHelmet);
            recipe.AddIngredient(ItemID.CrystalShard, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}