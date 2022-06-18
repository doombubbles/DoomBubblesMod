using DoomBubblesMod.Content.Projectiles.Ranged;
using DoomBubblesMod.Utils;
using ElementalDamage.Content.DamageClasses;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Items.Weapons;

public class TerraRifle : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("50% chance to not consume ammo\n" +
                           "Turns Bullets into Terra Bullets");
        DisplayName.SetDefault("Terra Rifle");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.damage = 42;
        Item.DamageType = GetInstance<RangedNature>();
        Item.width = 64;
        Item.height = 22;
        Item.useTime = 9;
        Item.useAnimation = 9;
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
        //recipe.AddIngredient(ModContent.ItemType<HeartOfTerraria>());
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextFloat() >= .50f && base.CanConsumeAmmo(ammo, player);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        type = ProjectileType<TerraBullet>();


        if (Main.rand.NextFloat() <= .2)
        {
            Projectile.NewProjectile(source, position, velocity, ProjectileType<MidnightBlast>(),
                damage * 2, knockback, player.whoAmI);
            SoundEngine.PlaySound(SoundID.Item38, position);
        }
        /* Other implementation
        else
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                // If you want to randomize the speed to stagger the projectiles
                float scale = 1f - (Main.rand.NextFloat() * .1f);
                perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<TerraBullet>(), (int)(.5 * damage), knockBack, player.whoAmI);
            }
        }
        */

        return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }


    // Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2, 0);
    }
}