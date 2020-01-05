using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class StatikkShiv : ModItem
    {
        public override void SetStaticDefaults()
        {   
            Tooltip.SetDefault("Energized - Electroshock\n" +
                               "Energized attacks bounce to 7 targets\n" +
                               "Equipped - 10% crit chance, attack speed, and move speed");
        }
        
        public override void SetDefaults()
        {
            item.damage = 70;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 3;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 26);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;

            item.shoot = mod.ProjectileType("Shiv");
            item.shootSpeed = 0f;
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().statikk = true;
            base.HoldItem(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<LoLPlayer>().statikk = true;
            player.meleeCrit += 10;
            player.magicCrit += 10;
            player.rangedCrit += 10;
            player.thrownCrit += 10;
            Mod gottaGoFast = ModLoader.GetMod("GottaGoFast");
            float speed = .1f;
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

            player.moveSpeed += .1f;
            player.maxRunSpeed += 1f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            position = Main.MouseWorld;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.GetModPlayer<LoLPlayer>().energized += 6;
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Zeal"));
            recipe.AddIngredient(mod.ItemType("KircheisShard"));
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}