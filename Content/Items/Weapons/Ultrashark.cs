using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Weapons;

public class Ultrashark : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("50% chance to not consume ammo\n" +
                           "'It came from the edge of Minishark's cool uncle'");
        DisplayName.SetDefault("Ultrashark");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.damage = 65;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 90;
        Item.height = 32;
        Item.useTime = 6;
        Item.useAnimation = 6;
        Item.crit = 5;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4;
        Item.value = Item.sellPrice(0, 10, 50);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 11f;
        Item.useAmmo = AmmoID.Bullet;
        Item.scale = .9f;
    }

    /*public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
    {
        // Here we use the multiplicative damage modifier because Terraria does this approach for Ammo damage bonuses. 
        mult *= player.bulletDamage;
    }*/

    // What if I wanted this gun to have a 38% chance not to consume ammo?
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextFloat() >= .50f && base.CanConsumeAmmo(ammo, player);
    }


    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        return base.Shoot(player, source, position, velocity.RotatedByRandom(MathHelper.ToRadians(3)), type, damage,
            knockback);
    }


    // Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2, -3);
    }
}