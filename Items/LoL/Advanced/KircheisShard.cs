using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class KircheisShard : ModItem
    {
        public override void SetStaticDefaults()
        {   
            Tooltip.SetDefault("Energized - Jolt\n" +
                               "Energized attacks deal 180 bonus damage\n" +
                               "Equipped - 5% increased attack speed");
        }
        
        public override void SetDefaults()
        {
            item.damage = 30;
            item.melee = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 3;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 7);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().jolt = true;
            base.HoldItem(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<LoLPlayer>().jolt = true;
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
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.GetModPlayer<LoLPlayer>().energized += 6;
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Dagger"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}