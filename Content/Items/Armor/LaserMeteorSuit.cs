namespace DoomBubblesMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Body)]
public class LaserMeteorSuit : ModItem
{
    public override void SetStaticDefaults()
    {
        // Tooltip.SetDefault("11% Increased Magic Damage");
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        var realSlot = Item.bodySlot;
        Item.CloneDefaults(ItemID.MeteorSuit);
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 4, 50);
        Item.defense = 12;
        Item.bodySlot = realSlot;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += .11f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MeteorSuit);
        recipe.AddIngredient(ItemID.CrystalShard, 20);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}