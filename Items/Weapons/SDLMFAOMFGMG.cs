using DoomBubblesMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Weapons
{
    public class SDLMFAOMFGMG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("S.D.L.M.F.A.O.M.F.G.M.G.");
            Tooltip.SetDefault("It came from the edge of Calamity\n" +
                               "75% chance to not consume ammo");
            Item.SetResearchAmount(1);
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

        public override bool ConsumeAmmo(Player player)
        {
            return !(Main.rand.NextDouble() <= .75) && base.ConsumeAmmo(player);
        }

        /*public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat)
        {
            damage.Scale(player.bulletDamage);
        }*/

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type,
            int damage, float knockback)
        {
            var dub = Main.rand.NextDouble();
            switch (dub)
            {
                case > .66:
                {
                    var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 1.5f,
                        ModContent.ProjectileType<TerraBullet>(),
                        damage, knockback, player.whoAmI);
                    projecitle.netUpdate = true;
                    break;
                }
                case < .33:
                {
                    var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 1.5f,
                        ModContent.ProjectileType<TrueHomingBullet>(),
                        damage, knockback, player.whoAmI);
                    projecitle.netUpdate = true;
                    break;
                }
                default:
                {
                    var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 1.5f,
                        ModContent.ProjectileType<TruePiercingBullet>(),
                        damage, knockback, player.whoAmI);
                    projecitle.netUpdate = true;
                    break;
                }
            }


            if (DoomBubblesMod.calamityMod != null)
            {
                FireCalamityProjectiles(source, position, velocity, damage, knockback, player);
            }

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        private void FireCalamityProjectiles(ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int damage, float knockBack,
            Player player)
        {
            var dub = Main.rand.NextDouble();
            if (dub > .66)
            {
                var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 3,
                    DoomBubblesMod.calamityMod.Find<ModProjectile>("FishronRPG").Type,
                    damage, knockBack, player.whoAmI);
                projecitle.netUpdate = true;
            }
            else if (dub < .33)
            {
                var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 2,
                    DoomBubblesMod.calamityMod.Find<ModProjectile>("BloodfireBullet").Type,
                    damage, knockBack, player.whoAmI);
                projecitle.netUpdate = true;
            }
            else
            {
                var projecitle = Projectile.NewProjectileDirect(source, position, velocity * 2,
                    DoomBubblesMod.calamityMod.Find<ModProjectile>("AstralRound").Type,
                    damage, knockBack, player.whoAmI);
                projecitle.netUpdate = true;
            }
        }

        public override void AddRecipes()
        {
            if (DoomBubblesMod.calamityMod != null)
            {
                AddCalamityRecipe();
            }
        }

        private void AddCalamityRecipe()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(DoomBubblesMod.calamityMod.Find<ModItem>("SDFMG"));
            recipe.AddIngredient(DoomBubblesMod.calamityMod.Find<ModItem>("ClaretCannon"));
            recipe.AddIngredient(DoomBubblesMod.calamityMod.Find<ModItem>("StormDragoon"));
            recipe.AddIngredient(DoomBubblesMod.calamityMod.Find<ModItem>("AstralBlaster"));
            recipe.AddIngredient(ModContent.ItemType<TerraRifle>());
            recipe.AddIngredient(ModContent.ItemType<Ultrashark>());
            recipe.AddIngredient(DoomBubblesMod.calamityMod.Find<ModItem>("NightmareFuel"), 5);
            recipe.AddIngredient(DoomBubblesMod.calamityMod.Find<ModItem>("EndothermicEnergy"), 5);
            recipe.AddIngredient(DoomBubblesMod.calamityMod.Find<ModItem>("CosmiliteBar"), 5);
            recipe.AddIngredient(DoomBubblesMod.calamityMod.Find<ModItem>("Phantoplasm"), 5);
            recipe.AddIngredient(DoomBubblesMod.calamityMod.Find<ModItem>("HellcasterFragment"), 3);
            recipe.AddIngredient(DoomBubblesMod.calamityMod.Find<ModItem>("DarksunFragment"), 5);
            recipe.AddIngredient(DoomBubblesMod.calamityMod.Find<ModItem>("AuricOre"), 25);
            recipe.AddTile(DoomBubblesMod.calamityMod.Find<ModTile>("DraedonsForge").Type);
            recipe.Register();
        }
    }
}