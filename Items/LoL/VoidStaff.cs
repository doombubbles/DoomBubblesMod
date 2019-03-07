using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace DoomBubblesMod.Items.LoL
{
    public class VoidStaff : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 63;
            item.magic = true;
            item.mana = 20;
            item.width = 68;
            item.height = 56;

            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 6;
            item.value = 42069;
            item.rare = 8;
            item.UseSound = SoundID.Item43;
            item.autoReuse = true;
            item.shootSpeed = 10f;
            item.shoot = 294;
            Item.staff[item.type] = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Staff");
            Tooltip.SetDefault("Hi Oliver");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShadowbeamStaff, 1);
            recipe.AddIngredient(ItemID.InfernoFork, 1);
            recipe.AddIngredient(ItemID.SpectreStaff, 1);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.AddTile(TileID.Dirt);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 295, damage, knockBack, Main.myPlayer);
            Main.PlaySound(2, position, 72);
            Main.PlaySound(2, position, 73);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 297, damage, knockBack, Main.myPlayer);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

    }
}