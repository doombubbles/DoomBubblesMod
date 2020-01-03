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
            Tooltip.SetDefault("If you've hit with a magic attack in the last 2 seconds,\n" +
                               "your melee damage is increased by your total magic damage.\n" +
                               "Equipped - 25 mana, 10% cdr, move speed and magic damage");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 10, 50);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.scale = 1.2f;
        }
        
        public override bool UseItem(Player player)
        {
            if (item.useStyle == 3)
            {
                item.useStyle = 1;
            }
            else
            {
                item.useStyle = 3;
            }
            return base.UseItem(player);
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
            player.statManaMax2 += 25;
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            player.magicDamage += .1f;
            player.moveSpeed += .1f;
            player.maxRunSpeed += 1f;
            if (player.HasBuff(mod.BuffType("LichBane"))) player.meleeDamage += player.magicDamage;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            player.magicDamage += .1f;
            player.moveSpeed += .1f;
            player.maxRunSpeed += 1f;
            base.HoldItem(player);
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (player.HasBuff(mod.BuffType("LichBane"))) add += player.magicDamage;
            base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            player.GetModPlayer<LoLPlayer>().lichBane = true;
            base.UpdateInventory(player);
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