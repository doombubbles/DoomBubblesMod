using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace DoomBubblesMod.Items.LoL
{
    class Firecannon : ModItem
    {
        public override void SetDefaults()
        {

            item.value = 640000;


            item.rare = 8;
            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 5;
            item.reuseDelay = 15;
            item.width = 48;
            item.height = 24;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.damage = 30;
            item.shootSpeed = 8.0f;
            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Firecannon");
          Tooltip.SetDefault("Three round burst\nFires a spread of bullets");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ClockworkAssaultRifle, 1);
            recipe.AddIngredient(ItemID.Shotgun, 1);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.AddTile(TileID.AmmoBox);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                return base.ConsumeAmmo(player);
            }
            else
            {
                return false;
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            float posX = Main.screenWidth / 2;
            float posY = Main.screenHeight / 2;
            float buh = 1f;

            if (Main.mouseY < Main.screenHeight / 2 && player.direction == 1)
            {
                buh = -1f;
            }
            else if (Main.mouseY > Main.screenHeight / 2 && player.direction == -1)
            {
                buh = -1f;
            }

            float slope = -1 / ((Main.mouseY - posY) / (Main.mouseX - posX));

            double numX = 4 * Math.Cos(Math.Atan(slope));

            double numY = 4 * Math.Sin(Math.Atan(slope));

            int spread = 30;
            float spreadMult = 0.05f;
            for (int i = 0; i < 3; ++i)
            {
                float vX = speedX + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = speedY + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
                Projectile.NewProjectile(position.X + (float)numX * buh, position.Y + (float)numY * buh, vX, vY, type, damage, knockBack, Main.player[Main.myPlayer].whoAmI);
                Main.PlaySound(2, position, 38);
            }
            return false;
        }


        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return PrefixID.Rapid;
        }
    }
}
