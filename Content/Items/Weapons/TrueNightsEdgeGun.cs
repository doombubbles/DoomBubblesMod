using DoomBubblesMod.Content.Projectiles.Ranged;
using ElementalDamage.Common.Types;
using ElementalDamage.Content.DamageClasses;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Items.Weapons;

public abstract class TrueNightsEdgeGun<T> : ModItem where T : MidnightBlast
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.damage = 40;
        Item.DamageType = ElementalDamageClass.Get<RangedDamageClass, Shadow>();
        Item.width = 58;
        Item.height = 24;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 4;
        Item.value = Item.sellPrice(0, 10);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 12f;
        Item.useAmmo = AmmoID.Bullet;
    }


    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item36, position);
        var numberProjectiles = 3 + Main.rand.Next(2);
        for (var i = 0; i < numberProjectiles; i++)
        {
            var perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
            // If you want to randomize the speed to stagger the projectiles
            var scale = 1f - Main.rand.NextFloat() * .1f;
            perturbedSpeed *= scale;
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type,
                (int) (damage / 2.0), knockback, player.whoAmI);
        }

        Projectile.NewProjectile(source, position, velocity, ProjectileType<T>(),
            damage, knockback, player.whoAmI);
        
        return false;
    }


    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2, 0);
    }
}