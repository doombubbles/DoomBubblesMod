using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class Gunblade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hextech Gunblade");
            Tooltip.SetDefault("15% Lifesteal\nStronger against more powerful enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 65;
            item.melee = true;
            item.ranged = true;
            item.width = 54;
            item.height = 54;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 34);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.useAmmo = AmmoID.Bullet;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 10f;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);
            player.GetModPlayer<LoLPlayer>().Lifesteal(damage * .15f, target, false);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BilgewaterCutlass"));
            recipe.AddIngredient(mod.ItemType("HextechRevolver"));
            recipe.AddIngredient(ItemID.GoldCoin, 7);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            //recipe.AddRecipe();
        }
        

    }
}
