using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class LaserMeteorHelmet : ModItem
{
    private const string SetBonus = "Laser Rifle costs 0 mana";

    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("11% Increased Magic Damage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        var realSlot = Item.headSlot;
        Item.CloneDefaults(ItemID.MeteorHelmet);
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 6, 75);
        Item.defense = 10;
        Item.headSlot = realSlot;
    }

    public override void Load()
    {
        SetBonusAccessories?.Call(this, "Laser Meteor", typeof(LaserMeteorHelmet), typeof(LaserMeteorLeggings),
            typeof(LaserMeteorLeggings), SetBonus);
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ItemType<LaserMeteorSuit>() &&
               legs.type == ItemType<LaserMeteorLeggings>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = SetBonus;
        player.spaceGun = true;
        player.GetModPlayer<DoomBubblesPlayer>().NoManaItems.Add(ItemID.LaserRifle);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += .11f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MeteorHelmet);
        recipe.AddIngredient(ItemID.CrystalShard, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}