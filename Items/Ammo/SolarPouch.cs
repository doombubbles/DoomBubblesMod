using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Ammo
{
    internal class SolarPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Solar Pouch");
            Tooltip.SetDefault("Deals bonus damage to airborne enemies");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<Projectiles.SolarBullet>();
            Item.width = 26;
            Item.height = 34;
            Item.ammo = AmmoID.Bullet;
            Item.value = Item.sellPrice(0, 4);
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ItemRarityID.Red;
            Item.damage = 17;
            Item.knockBack = 3;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SolarBullet>(), 3996);
            recipe.ReplaceResult(this);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}