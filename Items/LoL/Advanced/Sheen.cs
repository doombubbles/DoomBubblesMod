using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class Sheen : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("100% increased melee damage if you've dealt\n" +
                               "magic damage in the last 1.5 seconds\n" +
                               "Equipped - 25 mana and 10% cdr");
        }

        public override void SetDefaults()
        {
            item.damage = 35;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 10, 50);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.scale = 1.2f;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().sheen = true;
            player.statManaMax2 += 25;
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            if (player.HasBuff(mod.BuffType("Sheen"))) player.meleeDamage += 1f;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().cdr += .1f;
            base.HoldItem(player);
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (player.HasBuff(mod.BuffType("Sheen"))) add += 1f;
            base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            player.GetModPlayer<LoLPlayer>().sheen = true;
            base.UpdateInventory(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SapphireCrystal"));
            recipe.AddIngredient(ItemID.GoldCoin, 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}