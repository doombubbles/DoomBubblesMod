using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
public class LaserMeteorLeggings : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Laser Meteor Leggings");
        Tooltip.SetDefault("11% Increased Magic Damage");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        var realSlot = Item.legSlot;
        Item.CloneDefaults(ItemID.MeteorLeggings);
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 4, 50);
        Item.defense = 10;
        Item.legSlot = realSlot;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<LaserMeteorHelmet>() &&
               body.type == ModContent.ItemType<LaserMeteorSuit>();
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += .11f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MeteorLeggings);
        recipe.AddIngredient(ItemID.CrystalShard, 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}