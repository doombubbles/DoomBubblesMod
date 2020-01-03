using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class TitanicHydra : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanic Hydra");
            Tooltip.SetDefault("Damage scales with bonus health");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.melee = true;
            item.width = 52;
            item.height = 52;
            item.useTime = 40;
            item.scale = 1.1f;
            item.useAnimation = 40;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 35);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Cleave");
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().cleave = true;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Tiamat"));
            recipe.AddIngredient(mod.ItemType("RubyCrystal"));
            recipe.AddIngredient(mod.ItemType("JaurimsFist"));
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddIngredient(ItemID.SilverCoin, 75);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            flat += player.statLifeMax2 - player.statLifeMax;
            base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectileDirect(target.Center, new Vector2(0,0), mod.ProjectileType("Cleave"), damage, knockBack, player.whoAmI, 1);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
}
