using DoomBubblesMod.Content.Items.Misc;
using DoomBubblesMod.Content.Projectiles.Ranged;
using ElementalDamage.Common.Types;
using ElementalDamage.Content.DamageClasses;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Items.Weapons;

public class TerraRifle : ModItem
{
    private int counter;

    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("50% chance to not consume ammo\n" +
                           "Turns Musket Balls into Terra Bullets");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.damage = 42;
        Item.DamageType = ElementalDamageClass.Get<RangedDamageClass, Nature>();
        Item.width = 64;
        Item.height = 22;
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 6;
        Item.value = Item.sellPrice(0, 20);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item36;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 11f;
        Item.useAmmo = AmmoID.Bullet;
    }

    /*public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
    {
        // Here we use the multiplicative damage modifier because Terraria does this approach for Ammo damage bonuses. 
        mult *= player.bulletDamage;
    }*/

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemType<TrueMidnightMaelstrom>());
        recipe.AddIngredient(ItemType<TrueTrigun>());
        recipe.AddIngredient(ItemType<BrokenHeroGun>());
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();


        var recipe2 = CreateRecipe();
        recipe2.AddIngredient(ItemType<TrueBloodburstBlunderbuss>());
        recipe2.AddIngredient(ItemType<TrueTrigun>());
        recipe2.AddIngredient(ItemType<BrokenHeroGun>());
        recipe2.AddTile(TileID.MythrilAnvil);
        recipe2.Register();
    }

    public override bool CanConsumeAmmo(Item ammo, Player player) => Main.rand.NextFloat() >= .50f;

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            counter++;
            if (counter >= 7)
            {
                var proj = Projectile.NewProjectileDirect(source, position, velocity, ProjectileType<MidnightBlast>(),
                    damage * 2, knockback, player.whoAmI);
                proj.netUpdate = true;
                SoundEngine.PlaySound(SoundID.Item38, position);
                counter = 0;
            }
        }

        return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
        ref float knockback)
    {
        if (type == ProjectileID.Bullet)
        {
            type = ProjectileType<TerraBullet>();
        }
        
        base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2, 0);
    }
}