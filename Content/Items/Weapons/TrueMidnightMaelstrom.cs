using DoomBubblesMod.Content.Items.Misc;
using DoomBubblesMod.Content.Projectiles.Ranged;
using DoomBubblesMod.Utils;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace DoomBubblesMod.Content.Items.Weapons;

public class TrueMidnightMaelstrom : ModItem
{
    public override void SetStaticDefaults()
    {
        //Tooltip.SetDefault("This is a modded gun.");
        DisplayName.SetDefault("True Midnight Maelstrom");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.damage = 60;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 58;
        Item.height = 24;
        Item.useTime = 27;
        Item.useAnimation = 27;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4;
        Item.value = Item.sellPrice(0, 10);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = false;
        Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 10f;
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
        recipe.AddIngredient(ModContent.ItemType<MidnightMaelstrom>());
        recipe.AddIngredient(ModContent.ItemType<BrokenHeroGun>());
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item36, position);
        var numberProjectiles = 3 + Main.rand.Next(2);
        for (var i = 0; i < numberProjectiles; i++)
        {
            var perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
            // If you want to randomize the speed to stagger the projectiles
            var scale = 1f - Main.rand.NextFloat() * .1f;
            perturbedSpeed = perturbedSpeed * scale;
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type,
                (int) (damage / 2.0), knockback, player.whoAmI);
        }

        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<MidnightBlast>(),
            damage, knockback, player.whoAmI);


        return false;
    }


    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2, 0);
    }
}