using DoomBubblesMod.Content.Projectiles.Ranged;

namespace DoomBubblesMod.Content.Items.Weapons;

public class SDLMFAOMFGMG : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("S.D.L.M.F.A.O.M.F.G.M.G.");
        Tooltip.SetDefault("It came from the edge of Calamity\n" +
                           "75% chance to not consume ammo");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.damage = 640;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 108;
        Item.height = 42;
        Item.useTime = 3;
        Item.useAnimation = 3;
        Item.crit = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4;
        Item.value = Item.sellPrice(0, 50);
        Item.rare = ItemRarityID.Purple;
        Item.expert = true;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 11f;
        Item.useAmmo = AmmoID.Bullet;
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(0, -5);
    }

    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextDouble() <= .75;
    }

    /*public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat)
    {
        damage.Scale(player.bulletDamage);
    }*/

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        var dub = Main.rand.NextDouble();
        switch (dub)
        {
            case > .66:
            {
                var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 1.5f,
                    ProjectileType<TerraBullet>(),
                    damage, knockback, player.whoAmI);
                projecitle.netUpdate = true;
                break;
            }
            case < .33:
            {
                var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 1.5f,
                    ProjectileType<TrueHomingBullet>(),
                    damage, knockback, player.whoAmI);
                projecitle.netUpdate = true;
                break;
            }
            default:
            {
                var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 1.5f,
                    ProjectileType<TruePiercingBullet>(),
                    damage, knockback, player.whoAmI);
                projecitle.netUpdate = true;
                break;
            }
        }


        if (CalamityMod != null)
        {
            FireCalamityProjectiles(source, position, velocity, damage, knockback, player);
        }

        return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }

    private void FireCalamityProjectiles(EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int damage, float knockBack,
        Player player)
    {
        var dub = Main.rand.NextDouble();
        if (dub > .66)
        {
            var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 3,
                CalamityMod.Find<ModProjectile>("FishronRPG").Type,
                damage, knockBack, player.whoAmI);
            projecitle.netUpdate = true;
        }
        else if (dub < .33)
        {
            var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 2,
                CalamityMod.Find<ModProjectile>("BloodfireBullet").Type,
                damage, knockBack, player.whoAmI);
            projecitle.netUpdate = true;
        }
        else
        {
            var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 2,
                CalamityMod.Find<ModProjectile>("AstralRound").Type,
                damage, knockBack, player.whoAmI);
            projecitle.netUpdate = true;
        }
    }

    public override void AddRecipes()
    {
        if (CalamityMod != null)
        {
            AddCalamityRecipe();
        }
    }

    private void AddCalamityRecipe()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(CalamityMod.Find<ModItem>("SDFMG"));
        recipe.AddIngredient(CalamityMod.Find<ModItem>("ClaretCannon"));
        recipe.AddIngredient(CalamityMod.Find<ModItem>("StormDragoon"));
        recipe.AddIngredient(CalamityMod.Find<ModItem>("AstralBlaster"));
        recipe.AddIngredient(ItemType<TerraRifle>());
        recipe.AddIngredient(ItemType<Ultrashark>());
        recipe.AddIngredient(CalamityMod.Find<ModItem>("NightmareFuel"), 5);
        recipe.AddIngredient(CalamityMod.Find<ModItem>("EndothermicEnergy"), 5);
        recipe.AddIngredient(CalamityMod.Find<ModItem>("CosmiliteBar"), 5);
        recipe.AddIngredient(CalamityMod.Find<ModItem>("Phantoplasm"), 5);
        recipe.AddIngredient(CalamityMod.Find<ModItem>("HellcasterFragment"), 3);
        recipe.AddIngredient(CalamityMod.Find<ModItem>("DarksunFragment"), 5);
        recipe.AddIngredient(CalamityMod.Find<ModItem>("AuricOre"), 25);
        recipe.AddTile(CalamityMod.Find<ModTile>("DraedonsForge").Type);
        recipe.Register();
    }
}