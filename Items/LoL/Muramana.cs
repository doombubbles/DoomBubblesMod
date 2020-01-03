using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class Muramana : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased damage based on your max mana\n" +
                               "Nonmagic damage expends mana to deal bonus damage\n" +
                               "Equipped - 15% reduced mana usage and 100 bonus mana");
        }

        public override void SetDefaults()
        {
            item.damage = 35;
            item.melee = true;
            item.width = 56;
            item.height = 56;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 33);
            item.rare = 9;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.scale = 1.2f;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            flat += (int) (player.statManaMax2 * .3);
            base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += player.statManaMax2 * .0005f;
            player.rangedDamage += player.statManaMax2 * .0005f;
            player.thrownDamage += player.statManaMax2 * .0005f;
            player.minionDamage += player.statManaMax2 * .0005f;
            player.manaCost -= .15f;
            player.statManaMax2 += 100;
            player.GetModPlayer<LoLPlayer>().muramana = true;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
            player.GetModPlayer<LoLPlayer>().muramana = true;
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Manamune"));
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 59);
            }
        }
        

    }
}
