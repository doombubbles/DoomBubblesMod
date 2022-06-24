using Terraria.Audio;

namespace DoomBubblesMod.Content.Items.Weapons;

public abstract class NightsEdgeGun : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 20;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 52;
        Item.height = 20;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4;
        Item.value = 54000;
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 10f;
        Item.useAmmo = AmmoID.Bullet;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item36, position);
        var numberProjectiles = 2 + Main.rand.Next(2);
        for (var i = 0; i < numberProjectiles; i++)
        {
            var perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
            // If you want to randomize the speed to stagger the projectiles
            var scale = 1f - Main.rand.NextFloat() * .1f;
            perturbedSpeed = perturbedSpeed * scale;
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type,
                (int) (damage / 2.0), knockback, player.whoAmI);
        }

        return true;
    }
}