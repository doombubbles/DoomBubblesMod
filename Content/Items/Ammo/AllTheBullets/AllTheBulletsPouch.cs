using DoomBubblesMod.Content.Projectiles.Ranged.AllTheBullets;
using ElementalDamage.Common.Types;

namespace DoomBubblesMod.Content.Items.Ammo.AllTheBullets;

public abstract class AllTheBulletsPouch<T> : ModItem where T : AllTheBullet
{
    protected abstract short SourceItem { get; }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.EndlessMusketPouch);
        Item.shoot = ProjectileType<T>();
        var source = new Item(SourceItem);
        Item.value = source.value;
        Item.rare = source.rare;
        Item.damage = 5 + source.rare;
        Item.crit = source.crit;
        Item.shootSpeed = source.rare / 2.0f;
        Item.knockBack = source.knockBack / 3f;
        if (source.DamageType is MultiDamageClass multiDamageClass)
        {
            Item.DamageType = ElementalDamageClass.Get(DamageClass.Ranged, multiDamageClass.ElementalDamageClass);
        }
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(SourceItem)
        .AddTile(TileID.AmmoBox)
        .Register();
}