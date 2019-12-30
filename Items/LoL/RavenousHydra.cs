using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class RavenousHydra : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ravenous Hydra");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.melee = true;
            item.width = 50;
            item.height = 50;
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
            item.shoot = mod.ProjectileType("Crescent");
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().crescent = true;
            player.GetModPlayer<LoLPlayer>().crescentLifeSteal = true;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Tiamat"));
            recipe.AddIngredient(mod.ItemType("VampiricScepter"));
            recipe.AddIngredient(mod.ItemType("PickaxeOfLegends"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            Projectile.NewProjectileDirect(position, new Vector2(0,0), mod.ProjectileType("Crescent"), damage, knockBack, player.whoAmI, 0, 1);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectileDirect(target.Center, new Vector2(0,0), mod.ProjectileType("Crescent"), damage, knockBack, player.whoAmI, 1, 1);
            player.GetModPlayer<LoLPlayer>().Lifesteal(damage * .12f, target, false);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
}
