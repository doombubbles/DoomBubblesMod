using System;
using DoomBubblesMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Weapons
{
    public class TrueTrigun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("25% chance not to consume ammo\n."
                               + "Shoots in three round bursts with True Bullets\n");
            DisplayName.SetDefault("True Trigun");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 56;
            Item.height = 26;
            Item.useTime = 3;
            Item.useAnimation = 9;
            Item.reuseDelay = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 10);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = false;
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
            if (DoomBubblesMod.thoriumMod != null)
            {
                addThoriumRecipe();
            }
            else
            {
                var recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.HallowedBar, 9);
                recipe.AddIngredient(ModContent.ItemType<BrokenHeroGun>());
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.ReplaceResult(this);
                recipe.Register();
            }
        }

        private void addThoriumRecipe()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(DoomBubblesMod.thoriumMod.Find<ModItem>("Trigun"));
            recipe.AddIngredient(ModContent.ItemType<BrokenHeroGun>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.NextFloat() <= 25f)
            {
                return false;
            }

            return player.itemAnimation < Item.useAnimation - 2 && base.ConsumeAmmo(player);
        }

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type,
            int damage, float knockback)
        {
            var perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));

            var to8 = Math.Abs(player.itemAnimation - 8);
            var to5 = Math.Abs(player.itemAnimation - 5);
            var to2 = Math.Abs(player.itemAnimation - 2);
            var closest = Math.Min(Math.Min(to2, to5), to8);

            if (closest == to8)
            {
                type = ModContent.ProjectileType<TruePiercingBullet>();
            }
            else if (closest == to2)
            {
                type = ModContent.ProjectileType<TrueHomingBullet>();
            }

            return base.Shoot(player, source, position, perturbedSpeed, type, damage, knockback);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
    }
}