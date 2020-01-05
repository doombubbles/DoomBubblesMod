using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class Zeal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Equipped - 5% crit chance, attack speed, and move speed");
        }

        public override void SetDefaults()
        {
            item.damage = 37;
            item.melee = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 3;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 14);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.scale = 1.2f;
            item.crit = 21;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.meleeCrit += 5;
            player.magicCrit += 5;
            player.rangedCrit += 5;
            player.thrownCrit += 5;
            Mod gottaGoFast = ModLoader.GetMod("GottaGoFast");
            float speed = .05f;
            if(gottaGoFast != null)
            {
                //First Argument is a string for the type; either "magicSpeed", "rangedSpeed" or "attackSpeed"
                //Second Argument is an int for the index of the player in question; You can get that using player.whoAmI
                //Third argument is a float for the value to add; e.g. .1f for 10% increase, -.05f for 5% decrease
                gottaGoFast.Call("attackSpeed", player.whoAmI, speed);
            } else {
                //If the player doesn't have the mod, just increase melee speed
                player.meleeSpeed += speed;
            }

            player.moveSpeed += .05f;
            player.maxRunSpeed += .5f;
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            item.accessory = true;
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
            spriteBatch.Draw(mod.GetTexture("Items/LoL/Advanced/Zeal"), position + new Vector2(0, mod.GetTexture("Items/LoL/Advanced/Zeal").Height * scale), frame, drawColor, MathHelper.Pi / -2, origin, scale, SpriteEffects.None, 0f);
        }
        /*
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale,
            int whoAmI)
        {
            float num4 = item.height - Main.itemTexture[item.type].Height;
            float num5 = item.width / 2 - Main.itemTexture[item.type].Width / 2;
            Vector2 pos =
                new Vector2(
                    item.position.X - Main.screenPosition.X + (float) (Main.itemTexture[item.type].Width / 2) + num5,
                    item.position.Y - Main.screenPosition.Y + (float) (Main.itemTexture[item.type].Height / 2) + num4 + 2f);
            base.PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
            spriteBatch.Draw(mod.GetTexture("Items/LoL/Advanced/Zeal"), pos + new Vector2(0, mod.GetTexture("Items/LoL/Advanced/Zeal").Height * scale), null, lightColor, rotation + MathHelper.Pi / -2, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
        */

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CloakOfAgility"));
            recipe.AddIngredient(mod.ItemType("Dagger"));
            recipe.AddIngredient(ItemID.GoldCoin, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}