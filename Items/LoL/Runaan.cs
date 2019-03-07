using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    class Runaan : ModItem
    {
        public override void SetDefaults()
        {

            item.value = 640000;


            item.rare = 8;
            item.useStyle = 5;
            item.useAnimation = 18;
            item.width = 22;
            item.height = 44;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.damage = 80;
            item.shootSpeed = 10.0f;
            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;
            item.useTime = 18;
            item.UseSound = SoundID.Item5;
            item.knockBack = 2;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Runaan's Hurricane");
      Tooltip.SetDefault("Shoots additional homing bolts\n50% chance not to consume ammo");
    }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DaedalusStormbow, 1);
            recipe.AddIngredient(ItemID.LivingFireBlock, 40);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(1, 3) == 1)
            {
                return false;
            }
            else return base.ConsumeAmmo(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(5, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            float posX = Main.screenWidth / 2;
            float posY = Main.screenHeight / 2;

            float slope = -1 / ((Main.mouseY - posY) / (Main.mouseX - posX));

            double numX = 15 * Math.Cos(Math.Atan(slope));

            double numY = 15 * Math.Sin(Math.Atan(slope));

            Projectile.NewProjectile(position.X + (float)numX, position.Y + (float)numY, speedX, speedY, mod.ProjectileType("Hurricane"), damage, 0.0f, player.whoAmI);
            Projectile.NewProjectile(position.X - (float)numX, position.Y - (float)numY, speedX, speedY, mod.ProjectileType("Hurricane"), damage, 0.0f, player.whoAmI);
            return true;
        }
    }
}
