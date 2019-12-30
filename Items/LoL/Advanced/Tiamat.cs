using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class Tiamat : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tiamat");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.damage = 25;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 40;
            item.scale = 1.1f;
            item.useAnimation = 40;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 13, 25);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Crescent");
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().crescent = true;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LongSword"));
            recipe.AddIngredient(mod.ItemType("RejuvenationBead"));
            recipe.AddIngredient(mod.ItemType("LongSword"));
            recipe.AddIngredient(ItemID.GoldCoin, 4);
            recipe.AddIngredient(ItemID.SilverCoin, 75);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectileDirect(target.Center, new Vector2(0,0), mod.ProjectileType("Crescent"), damage, knockBack, player.whoAmI, 1);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
}
