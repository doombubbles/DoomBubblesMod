namespace DoomBubblesMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
public class LaserMeteorLeggings : ModItem
{
    public override void SetStaticDefaults()
    {
        // Tooltip.SetDefault("11% Increased Magic Damage");
        Item.ResearchUnlockCount = 1;
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