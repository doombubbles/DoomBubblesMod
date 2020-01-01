using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    class HextechRevolver : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Also scales with magic bonuses");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 10, 50);
            item.rare = 4;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.width = 48;
            item.height = 18;
            item.shoot = 1;
            item.useAmmo = AmmoID.Bullet;
            item.damage = 40;
            item.shootSpeed = 16.0f;
            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = false;
            item.UseSound = SoundID.Item41;
            item.knockBack = 5;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AmplifyingTome"));
            recipe.AddIngredient(mod.ItemType("AmplifyingTome"));
            recipe.AddIngredient(ItemID.GoldCoin, 1);
            recipe.AddIngredient(ItemID.SilverCoin, 80);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += player.magicDamage - 1;
            mult *= player.magicDamageMult;
            base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += player.magicCrit;
            base.GetWeaponCrit(player, ref crit);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(5, 0);
        }
        
    }
}