using IL.Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace DoomBubblesMod.Items.LoL
{
    public class TriForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trinity Force");
            Tooltip.SetDefault("Increases maximum life and mana by 25\n" +
                               "20% increased cooldown reduction and attack speed\n" +
                               "5% increased melee damage and movement speed\n" +
                               "Rage and Empowered Spellblade effects");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 37, 33);
            item.width = 46;
            item.height = 48;
            item.rare = 4;

            item.damage = 73;
            item.useStyle = 4;
            item.useTime = 7;
            item.useAnimation = 7;
            item.melee = true;
            item.shoot = mod.ProjectileType("TriStinger");
            item.shootSpeed = 13f;
            item.scale = .7f;
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            item.accessory = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            if (type == mod.ProjectileType("TriStinger")) item.shoot = mod.ProjectileType("TriSheen");
            else if (type == mod.ProjectileType("TriSheen")) item.shoot = mod.ProjectileType("TriPhage");
            else if (type == mod.ProjectileType("TriPhage")) item.shoot = mod.ProjectileType("TriStinger");
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 25;
            player.statManaMax2 += 25;
            player.GetModPlayer<LoLPlayer>().cdr += .2f;
            Mod gottaGoFast = ModLoader.GetMod("GottaGoFast");
            float speed = .2f;
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

            player.meleeDamage += .05f;
            player.moveSpeed += .05f;
            player.maxRunSpeed += .5f;
            player.GetModPlayer<LoLPlayer>().rage = true;
            player.GetModPlayer<LoLPlayer>().triforce = true;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Stinger"));
            recipe.AddIngredient(mod.ItemType("Sheen"));
            recipe.AddIngredient(mod.ItemType("Phage"));
            recipe.AddIngredient(ItemID.GoldCoin, 3);
            recipe.AddIngredient(ItemID.SilverCoin, 33);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}