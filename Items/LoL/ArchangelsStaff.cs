using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class ArchangelsStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Archangel's Staff");
            Tooltip.SetDefault("Increased damage based on your max mana\n" +
                               "Equipped - 25% reduced mana usage and 50-125 bonus mana\n" +
                               "and 20% cooldown reduction");
            
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.magic = true;
            item.width = 42;
            item.useStyle = 5;
            item.height = 44;
            item.useTime = 10;
            item.useAnimation = 10;
            item.mana = 6;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 32);
            item.rare = 8;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = ProjectileID.StarWrath;
            item.shootSpeed = 8f;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            flat += (int) (player.statManaMax2 * .1);
            base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().archangel = true;
            player.manaCost -= .25f;
            player.statManaMax2 += 50 + Math.Min(player.GetModPlayer<LoLPlayer>().tearStacks, 75);
            base.UpdateAccessory(player, hideVisual);
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            proj.melee = false;
            proj.magic = true;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Tear"));
            recipe.AddIngredient(mod.ItemType("LostChapter"));
            recipe.AddIngredient(ItemID.GoldCoin, 10);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        

    }
}
