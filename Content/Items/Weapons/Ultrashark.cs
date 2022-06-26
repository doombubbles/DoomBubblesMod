namespace DoomBubblesMod.Content.Items.Weapons;

public class Ultrashark : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("50% chance to not consume ammo\n" +
                           "'It came from the edge of Minishark's cool uncle'");
        SacrificeTotal = 1;
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
        Item.noMelee = true;
        Item.knockBack = 4;
        Item.value = Item.sellPrice(0, 10, 50);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 11f;
        Item.useAmmo = AmmoID.Bullet;
        Item.scale = .9f;
    }

    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextFloat() >= .50f && base.CanConsumeAmmo(ammo, player);
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
        velocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2, -3);
    }
}