namespace DoomBubblesMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
public class MartianMeteorLeggings : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("17% Increased Magic Damage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        var realSlot = Item.legSlot;
        Item.CloneDefaults(ItemID.MeteorLeggings);
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.sellPrice(0, 15);
        Item.defense = 15;
        Item.legSlot = realSlot;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += .17f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemType<LaserMeteorLeggings>());
        recipe.AddIngredient(ItemID.MartianConduitPlating, 75);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}