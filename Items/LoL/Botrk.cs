using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class Botrk : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade of the Ruined King");
            Tooltip.SetDefault("12% Lifesteal\nStronger against more powerful enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 65;
            item.melee = true;
            item.width = 54;
            item.height = 54;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 33);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Potrk");
            item.shootSpeed = 10f;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LoLPlayer>().botrk = true;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<LoLPlayer>().botrk = true;
            base.HoldItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);
            player.GetModPlayer<LoLPlayer>().Lifesteal(damage * .12f, target, false);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BilgewaterCutlass"));
            recipe.AddIngredient(mod.ItemType("RecurveBow"));
            recipe.AddIngredient(ItemID.GoldCoin, 7);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        

    }
}
