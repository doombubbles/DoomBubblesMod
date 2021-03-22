using Microsoft.Xna.Framework;
using Terraria;
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
        }

        public override void SetDefaults()
        {
            item.damage = 640;
            item.ranged = true;
            item.width = 108;
            item.height = 42;
            item.useTime = 3;
            item.useAnimation = 3;
            item.crit = 25;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 50);
            item.rare = 11;
            item.expert = true;
            item.UseSound = SoundID.Item40;
            item.autoReuse = true;
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 11f;
            item.useAmmo = AmmoID.Bullet;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -5);
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.NextDouble() <= .75)
            {
                return false;
            }

            return base.ConsumeAmmo(player);
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            // Here we use the multiplicative damage modifier because Terraria does this approach for Ammo damage bonuses. 
            mult *= player.bulletDamage;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage,
            ref float knockBack)
        {
            var velocity = new Vector2(speedX, speedY);

            var dub = Main.rand.NextDouble();
            if (dub > .66)
            {
                var projecitle = Projectile.NewProjectileDirect(position, velocity * 1.5f,
                    mod.ProjectileType("TerraBullet"),
                    damage, knockBack, player.whoAmI);
                projecitle.netUpdate = true;
            }
            else if (dub < .33)
            {
                var projecitle = Projectile.NewProjectileDirect(position, velocity * 1.5f,
                    mod.ProjectileType("TrueHomingBullet"),
                    damage, knockBack, player.whoAmI);
                projecitle.netUpdate = true;
            }
            else
            {
                var projecitle = Projectile.NewProjectileDirect(position, velocity * 1.5f,
                    mod.ProjectileType("TruePiercingBullet"),
                    damage, knockBack, player.whoAmI);
                projecitle.netUpdate = true;
            }


            if (DoomBubblesMod.calamityMod != null)
            {
                FireCalamityProjectiles(position, velocity, damage, knockBack, player);
            }

            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        private void FireCalamityProjectiles(Vector2 position, Vector2 velocity, int damage, float knockBack,
            Player player)
        {
            var dub = Main.rand.NextDouble();
            if (dub > .66)
            {
                var projecitle = Projectile.NewProjectileDirect(position, velocity * 3,
                    DoomBubblesMod.calamityMod.ProjectileType("FishronRPG"),
                    damage, knockBack, player.whoAmI);
                projecitle.netUpdate = true;
            }
            else if (dub < .33)
            {
                var projecitle = Projectile.NewProjectileDirect(position, velocity * 2,
                    DoomBubblesMod.calamityMod.ProjectileType("BloodfireBullet"),
                    damage, knockBack, player.whoAmI);
                projecitle.netUpdate = true;
            }
            else
            {
                var projecitle = Projectile.NewProjectileDirect(position, velocity * 2,
                    DoomBubblesMod.calamityMod.ProjectileType("AstralRound"),
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
            var recipe = new ModRecipe(mod);
            var CalamityMod = ModLoader.GetMod("CalamityMod");
            recipe.AddIngredient(CalamityMod.ItemType("SDFMG"));
            recipe.AddIngredient(CalamityMod.ItemType("ClaretCannon"));
            recipe.AddIngredient(CalamityMod.ItemType("StormDragoon"));
            recipe.AddIngredient(CalamityMod.ItemType("AstralBlaster"));
            recipe.AddIngredient(mod.ItemType("TerraRifle"));
            recipe.AddIngredient(mod.ItemType("Ultrashark"));
            recipe.AddIngredient(CalamityMod.ItemType("NightmareFuel"), 5);
            recipe.AddIngredient(CalamityMod.ItemType("EndothermicEnergy"), 5);
            recipe.AddIngredient(CalamityMod.ItemType("CosmiliteBar"), 5);
            recipe.AddIngredient(CalamityMod.ItemType("Phantoplasm"), 5);
            recipe.AddIngredient(CalamityMod.ItemType("HellcasterFragment"), 3);
            recipe.AddIngredient(CalamityMod.ItemType("DarksunFragment"), 5);
            recipe.AddIngredient(CalamityMod.ItemType("AuricOre"), 25);
            recipe.AddTile(CalamityMod.TileType("DraedonsForge"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}