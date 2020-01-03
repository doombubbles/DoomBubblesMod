using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class NashorsTooth : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nashor's Tooth");
            Tooltip.SetDefault("Also scales with magic bonuses\n" +
                               "Your melee damage also inflicts bonus magic damage\n" +
                               "20% increased cooldown reduction and attack speed");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 30);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.scale = 1.2f;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
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
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<LoLPlayer>().cdr += .2f;
            player.GetModPlayer<LoLPlayer>().nashor = true;
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void HoldItem(Player player)
        {
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
            player.GetModPlayer<LoLPlayer>().cdr += .2f;
            player.GetModPlayer<LoLPlayer>().nashor = true;
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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Stinger"));
            recipe.AddIngredient(mod.ItemType("FiendishCodex"));
            recipe.AddIngredient(ItemID.GoldCoin, 10);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}