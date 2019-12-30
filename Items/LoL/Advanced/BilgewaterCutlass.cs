using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class BilgewaterCutlass : ModItem
    {
        private int swing = 0;
        
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% Lifesteal");
        }

        public override void SetDefaults()
        {
            item.damage = 25;
            item.melee = true;
            item.width = 32;
            item.height = 42;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 16);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("BilgewaterCutlass");
            item.shootSpeed = 10f;
        }
        
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);
            player.GetModPlayer<LoLPlayer>().Lifesteal(damage * .1f, target, false);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            if (swing > 0)
            {
                swing--;
                return false;
            }
            swing = 2;
            damage *= 4;
            position += new Vector2(speedX, speedY);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VampiricScepter"));
            recipe.AddIngredient(mod.ItemType("LongSword"));
            recipe.AddIngredient(ItemID.GoldCoin, 3);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}