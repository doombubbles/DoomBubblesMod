using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class LichBane : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("After hitting with a projectile, your next swing deals bonus magic damage\n" +
                               "Equipped - 25 mana, 10% cdr, move speed and magic damage");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 32);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.scale = 1.2f;
            item.shoot = mod.ProjectileType("Bane");
            item.shootSpeed = 12f;
        }
        
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor,
            Vector2 origin, float scale)
        {
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
            spriteBatch.Draw(mod.GetTexture("Items/LoL/LichBane"), position + new Vector2(0, mod.GetTexture("Items/LoL/LichBane").Height * scale), frame, drawColor, MathHelper.Pi / -2, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().lichBane = true;
            player.GetModPlayer<LoLPlayer>().lichBane2 = true;
            player.statManaMax2 += 25;
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            player.magicDamage += .1f;
            player.moveSpeed += .1f;
            player.maxRunSpeed += 1f;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            player.magicDamage += .1f;
            player.moveSpeed += .1f;
            player.maxRunSpeed += 1f;
            player.GetModPlayer<LoLPlayer>().lichBane2 = true;
            base.HoldItem(player);
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            player.GetModPlayer<LoLPlayer>().lichBane = true;
            base.UpdateInventory(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            if (item.useStyle == 1)
            {
                item.useStyle = 3;
                return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
            }
            else
            {
                item.useStyle = 1;
                Vector2 actualSpawn = new Vector2(position.X, Main.screenPosition.Y);
			
                Vector2 velocity =
                    (Main.MouseWorld - actualSpawn).SafeNormalize(
                        (Main.MouseWorld - position).SafeNormalize(Vector2.Zero)) * new Vector2(speedX, speedY).Length();

                Projectile.NewProjectile(actualSpawn, velocity, type, damage, knockBack, player.whoAmI,
                    player.Center.Y - 200f);

                return false;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Sheen"));
            recipe.AddIngredient(mod.ItemType("AetherWisp"));
            recipe.AddIngredient(mod.ItemType("BlastingWand"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}