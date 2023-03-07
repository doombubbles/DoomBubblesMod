using System;
using DoomBubblesMod.Content.Projectiles.Ranged;
using ElementalDamage.Common.Types;
using ElementalDamage.Content.DamageClasses;

namespace DoomBubblesMod.Content.Items.Weapons;

public class TrueTrigun : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("25% chance not to consume ammo\nShoots in three round bursts with True Bullets");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.damage = 40;
        Item.DamageType = ElementalDamageClass.Get<RangedDamageClass, Holy>();
        Item.width = 56;
        Item.height = 26;
        Item.useTime = 4;
        Item.useAnimation = 12;
        Item.reuseDelay = 18;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4;
        Item.value = Item.sellPrice(0, 10);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 12f;
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
        if (ThoriumMod != null)
        {
            recipe.AddIngredient(ThoriumMod.Find<ModItem>("Trigun"));
        }
        else
        {
            recipe.AddIngredient(ItemID.HallowedBar, 12);
        }

        recipe.AddIngredient(ItemID.SoulofFright, 5);
        recipe.AddIngredient(ItemID.SoulofMight, 5);
        recipe.AddIngredient(ItemID.SoulofSight, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        if (Main.rand.NextFloat() <= 25f)
        {
            return false;
        }

        return player.itemAnimation < Item.useAnimation - 2;
    }
    
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
        ref float knockback)
    {
        velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));

        var progress = player.itemAnimation / (float) player.itemAnimationMax;
        type = progress switch
        {
            < .34f => ProjectileType<TruePiercingBullet>(),
            < .67f => ProjectileType<TrueHomingBullet>(),
            _ => type
        };
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2, 0);
    }
}