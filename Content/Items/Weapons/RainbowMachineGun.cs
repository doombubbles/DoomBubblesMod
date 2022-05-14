using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Weapons;

public class RainbowMachineGun : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 105;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 5;
        Item.width = 20;
        Item.height = 12;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2f;
        Item.value = Item.sellPrice(0, 15);
        Item.rare = ItemRarityID.Yellow;
        Item.autoReuse = true;
        Item.shootSpeed = 20f;
        Item.shoot = ProjectileType<Projectiles.Magic.RainbowMachineGun>();
        Item.channel = true;
        Item.noUseGraphic = true;
    }

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Rainbow Machinegun");
        Item.SetResearchAmount(1);
    }


    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LaserMachinegun);
        recipe.AddIngredient(ItemID.RainbowGun);
        recipe.AddIngredient(ItemID.LunarBar, 20);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}