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
            item.value = Item.buyPrice(0, 26);

            item.rare = 8;
            item.useStyle = 5;
            item.useAnimation = 30;
            item.useTime = 10;
            item.reuseDelay = 5;
            item.width = 48;
            item.height = 24;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.damage = 25;
            item.shootSpeed = 8.0f;
            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firecannon");
            Tooltip.SetDefault("Three round burst\nFires a spread of bullets\nEnergized - Sharpshooter\nEnergized bullets travel faster and always crit");
        }
        
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().sharpshooter = true;
            base.HoldItem(player);
        }
        
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat) {
            // Here we use the multiplicative damage modifier because Terraria does this approach for Ammo damage bonuses. 
            mult *= player.bulletDamage;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<LoLPlayer>().sharpshooter = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("KircheisShard"));
            recipe.AddIngredient(mod.ItemType("Zeal"));
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.Autohammer);
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

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX,
            ref float speedY, ref int type, ref int damage, ref float knockBack)
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
                float vX = speedX + (float) Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = speedY + (float) Main.rand.Next(-spread, spread + 1) * spreadMult;
                Projectile.NewProjectile(position.X + (float) numX * buh, position.Y + (float) numY * buh, vX, vY, type,
                    damage, knockBack, Main.player[Main.myPlayer].whoAmI);
                Main.PlaySound(2, position, 38);
            }

            player.GetModPlayer<LoLPlayer>().energized += 3;
            return false;
        }


        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return PrefixID.Rapid;
        }
    }
}